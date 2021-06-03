using System;
using System.Threading;
using System.Threading.Tasks;
using DomainModel.Services;
using DomainModel.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerServices
{
    public class ShowroomTracker: BackgroundService
    {
        private readonly IShowroomSettings _settings;
        private readonly IModelSynchronizer _modelSynchronizer;
        private readonly ILogger<ShowroomTracker> _logger;
        private readonly ICarSynchronizer _carSynchronizer;

        public ShowroomTracker(IShowroomSettings settings,
            IModelSynchronizer modelSynchronizer,
            ICarSynchronizer carSynchronizer,
            ILogger<ShowroomTracker> logger)
        {
            _settings = settings;
            _modelSynchronizer = modelSynchronizer;
            _carSynchronizer = carSynchronizer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SyncModels();

                await SyncCars();

                try
                {
                    await Task.Delay(_settings.RequestInterval, stoppingToken);
                }
                catch
                {
                    // ignored
                }
            }
        }

        async Task SyncModels()
        {
            try
            {
                await _modelSynchronizer.SyncModels();
            }
            catch (Exception ex)
            {
                _logger.LogError("Sync models exception: {0}", ex);
            }
        }


        async Task SyncCars()
        {
            try
            {
                await _carSynchronizer.SyncCars();
            }
            catch (Exception ex)
            {
                _logger.LogError("Sync cars exception: {0}", ex);
            }
        }
    }
}