using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IEmailSubscriberRepository
    {
        Task<IReadOnlyCollection<IEmailSubscriber>> GetAll();

        Task<IReadOnlyCollection<IEmailSubscriber>> GetEnabled();
    }
}