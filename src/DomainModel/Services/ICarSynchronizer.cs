using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface ICarSynchronizer
    {
        Task SyncCars();
    }
}