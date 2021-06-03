using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface ICarRepository
    {
        Task<ICar> GetByExternalId(int externalId);

        Task<ICar> Create(ICar car);

        Task<ICar> Update(ICar car);

        Task<IReadOnlyCollection<ICar>> GetAll();

        Task MarkAsDeleted(IReadOnlyCollection<ICar> missing);
    }
}