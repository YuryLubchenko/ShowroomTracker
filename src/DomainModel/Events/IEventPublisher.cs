using System.Collections.Generic;
using DomainModel.Entities;

namespace DomainModel.Events
{
    public interface IEventPublisher
    {
        void NewCarsAvailable(IReadOnlyCollection<ICar> cars);
    }
}