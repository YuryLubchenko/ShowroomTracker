using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface IModelSynchronizer
    {
        Task SyncModels();
    }
}