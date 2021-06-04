using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModel.Entities;

namespace Database.Entities
{
    [Table("TelegramSubscribers")]
    internal class TelegramSubscriber: ITelegramSubscriber
    {
        public TelegramSubscriber()
        {
        }

        public TelegramSubscriber(ITelegramSubscriber subscriber)
        {
            Id = subscriber.Id;
            ChatId = subscriber.ChatId;
            Disabled = subscriber.Disabled;
        }

        [Key]
        public int Id { get; set; }

        public long ChatId { get; set; }

        public bool Disabled { get; set; }
    }
}