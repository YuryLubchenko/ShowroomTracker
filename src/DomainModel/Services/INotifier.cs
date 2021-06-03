using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Services
{
    public interface INotifier
    {
        Task Notify(IReadOnlyCollection<ICar> newCars);
    }
}