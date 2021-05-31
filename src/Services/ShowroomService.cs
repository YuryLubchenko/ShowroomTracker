using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Services;
using DomainModel.Settings;
using Services.Entities.Api;
using Services.Entities.Domain;

namespace Services
{
    internal class ShowroomService: IShowroomService
    {
        private readonly HttpClient _httpClient;
        private readonly IShowroomSettings _settings;

        public ShowroomService(IShowroomSettings settings, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<IReadOnlyCollection<ICar>> GetAvailableCars()
        {
            var response = await _httpClient.GetAsync(_settings.GetCarsAction);

            if (!response.IsSuccessStatusCode)
                return new List<ICar>(0);

            var content = await response.Content.ReadAsStringAsync();

            var showroomResponse = JsonSerializer.Deserialize<ShowroomResponse>(content);

            return showroomResponse?.Models == null
                ? new List<ICar>(0)
                : showroomResponse.Models.Select(x => new Car(x)).OfType<ICar>().ToList();
        }

        public async Task<IReadOnlyCollection<IModel>> GetAvailableModels()
        {
            var response = await _httpClient.GetAsync(_settings.GetModelsAction);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            var showroomResponse = JsonSerializer.Deserialize<List<ShowroomModel>>(content);

            return showroomResponse == null
                ? new List<IModel>(0)
                : showroomResponse.Select(x => new Model(x)).OfType<IModel>().ToList();
        }
    }
}