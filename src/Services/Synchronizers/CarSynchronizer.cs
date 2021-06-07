using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Events;
using DomainModel.Repositories;
using DomainModel.Services;
using Services.Entities.Domain;

namespace Services.Synchronizers
{
    public class CarSynchronizer: ICarSynchronizer
    {
        private readonly IShowroomService _showroomService;
        private readonly ICarRepository _carRepository;
        private readonly IEventPublisher _eventPublisher;

        public CarSynchronizer(IShowroomService showroomService,
            ICarRepository carRepository,
            IEventPublisher eventPublisher)
        {
            _showroomService = showroomService;
            _carRepository = carRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task SyncCars()
        {
            var remoteCars = await _showroomService.GetAvailableCars();

            if (remoteCars == null)
                return;

            var newCars = new List<ICar>();

            foreach (var remoteCar in remoteCars)
            {
                var existingCar = await _carRepository.GetByExternalId(remoteCar.ExternalId);

                if (existingCar == null)
                {
                    var newCar = await _carRepository.Create(remoteCar);
                    newCars.Add(newCar);
                }
                else
                {
                    if (existingCar.Deleted)
                    {
                        newCars.Add(existingCar);
                    }

                    var c = new Car(remoteCar)
                    {
                        Id = existingCar.Id
                    };

                    await _carRepository.Update(c);
                }
            }

            var all = await _carRepository.GetAll();

            var remoteIds = remoteCars.Select(x => x.ExternalId).ToList();

            var missing = all.Where(x => !remoteIds.Contains(x.ExternalId) && !x.Deleted).ToList();

            await _carRepository.MarkAsDeleted(missing);

            if (newCars.Any())
            {
                _eventPublisher.NewCarsAvailable(newCars);
            }
        }
    }
}