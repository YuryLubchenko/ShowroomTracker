using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Repositories;
using DomainModel.Services;
using DomainModel.Settings;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Services.Notifiers
{
    public class TelegramClient: ITelegramClient, IDisposable
    {
        private readonly ITelegramNotifierSettings _settings;
        private readonly ILogger<TelegramClient> _log;
        private readonly ITelegramSubscriberRepository _repository;

        private ITelegramBotClient _bot;

        private readonly IDictionary<string, Func<long, string, Task>> _commands;

        public TelegramClient(ITelegramNotifierSettings settings,
            ITelegramSubscriberRepository repository,
            ILogger<TelegramClient> log)
        {
            _settings = settings;
            _repository = repository;
            _log = log;

            _commands = new Dictionary<string, Func<long, string, Task>>
            {
                {"/start", ProcessStartCommand},
                {"/help", ProcessHelpCommand},
                {"/settings", ProcessSettingsCommand},
                {"/subscribe", ProcessSubscribeCommand},
                {"/unsubscribe", ProcessUnsubscribeCommand}
            };

            Initialize();
        }

        private void Initialize()
        {
            _bot = new TelegramBotClient(_settings.BotToken);

            var me = _bot.GetMeAsync().GetAwaiter().GetResult();

            _log.LogInformation($"TelegramClient: bot Id {me.Id}, Username: {me.Username}");

            _bot.OnMessage += BotOnOnMessage;
            _bot.StartReceiving();
        }

        private async void BotOnOnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message.Type != MessageType.Text)
                return;

            var messageText = e.Message.Text;
            var chatId = e.Message.Chat.Id;

            _log.LogInformation($"TelegramClient: message from char {chatId} received: {messageText}");

            if (string.IsNullOrEmpty(messageText))
                return;

            if (!_commands.Keys.Any(x => messageText.ToLower().StartsWith(x)))
                await SendTextMessage(chatId, "Invalid command");

            var commandKey = _commands.Keys.FirstOrDefault(x => messageText.ToLower().StartsWith(x));

            if (string.IsNullOrEmpty(commandKey))
                return;

            await _commands[commandKey](chatId, messageText);
        }

        public async Task SendTextMessage(long chatId, string message)
        {
            await _bot.SendTextMessageAsync(chatId, message, ParseMode.Html);
        }

        private Task ProcessStartCommand(long chatId, string messageText)
        {
            var responseMessage = new StringBuilder();

            responseMessage.AppendLine("Hello there!");
            responseMessage.AppendLine("Please /subscribe to get notifications about new available cars");

            return _bot.SendTextMessageAsync(chatId, responseMessage.ToString());
        }

        private Task ProcessHelpCommand(long chatId, string messageText)
        {
            return Task.CompletedTask;
        }

        private Task ProcessSettingsCommand(long chatId, string messageText)
        {
            return Task.CompletedTask;
        }

        private async Task ProcessSubscribeCommand(long chatId, string messageText)
        {
            await _repository.Subscribe(chatId);

            var responseMessage = "You have successfully subscribed for notifications";
            await SendTextMessage(chatId, responseMessage);
        }

        private async Task ProcessUnsubscribeCommand(long chatId, string messageText)
        {
            await _repository.Unsubscribe(chatId);

            var responseMessage = "You have successfully unsubscribed from notifications";
            await SendTextMessage(chatId, responseMessage);
        }

        public void Dispose()
        {
            _bot.StopReceiving();
            _bot.OnMessage -= BotOnOnMessage;
        }
    }
}