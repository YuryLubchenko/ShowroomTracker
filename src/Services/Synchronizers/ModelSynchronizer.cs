using System.Linq;
using System.Threading.Tasks;
using DomainModel.Repositories;
using DomainModel.Services;
using Services.Entities.Domain;

namespace Services.Synchronizers
{
    internal class ModelSynchronizer: IModelSynchronizer
    {
        private readonly IShowroomService _showroomService;
        private readonly IModelRepository _modelRepository;

        public ModelSynchronizer(IShowroomService showroomService,
            IModelRepository modelRepository)
        {
            _showroomService = showroomService;
            _modelRepository = modelRepository;
        }

        public async Task SyncModels()
        {
            var remoteModels = await _showroomService.GetAvailableModels();

            if (remoteModels == null)
                return;

            foreach (var remoteModel in remoteModels)
            {
                var existingModel = await _modelRepository.GetByExternalId(remoteModel.ExternalId);

                if (existingModel == null)
                {
                    await _modelRepository.Create(remoteModel);
                }
                else
                {
                    var m = new Model(remoteModel)
                    {
                        Id = existingModel.Id
                    };

                    await _modelRepository.Update(m);
                }
            }

            var all = await _modelRepository.GetAll();

            var remoteIds = remoteModels.Select(x => x.ExternalId).ToList();

            var missing = all.Where(x => !remoteIds.Contains(x.ExternalId)).ToList();

            await _modelRepository.MarkAsDeleted(missing);
        }
    }
}