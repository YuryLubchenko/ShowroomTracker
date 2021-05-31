using System.Text.Json.Serialization;

namespace Services.Entities.Api
{
    public class ShowroomModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("modelInfoLink")]
        public string ModelInfoLink { get; set; }

        [JsonPropertyName("show")]
        public int Show { get; set; }

        [JsonPropertyName("is_subscribe_active")]
        public int IsSubscribeActive { get; set; }
    }
}