using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Services.Entities.Api
{
    public class ShowroomResponse
    {
        [JsonPropertyName("models")]
        public List<ShowroomCar> Models { get; set; }
    }
}