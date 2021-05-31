using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Services
{
    public interface IShowroomService
    {
        Task<IReadOnlyCollection<ICar>> GetAvailableCars();

        Task<IReadOnlyCollection<IModel>> GetAvailableModels();
    }
}