using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IModelRepository
    {
        Task<IModel> GetByExternalId(int externalId);

        Task<IModel> Create(IModel model);

        Task<IModel> Update(IModel model);

        Task<IReadOnlyCollection<IModel>> GetAll();

        Task MarkAsDeleted(IReadOnlyCollection<IModel> missing);
    }
}