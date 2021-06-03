using System.Text.Json.Serialization;

namespace Services.Entities.Api
{
    public class ShowroomCar
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("car_id")]
        public int CarId { get; set; }

        [JsonPropertyName("modification_id")]
        public int ModificationId { get; set; }

        [JsonPropertyName("complectation_id")]
        public int ComplectationId { get; set; }

        [JsonPropertyName("package_id")]
        public int PackageId { get; set; }

        [JsonPropertyName("color_exterior_id")]
        public int ColorExteriorId { get; set; }

        [JsonPropertyName("color_interior_id")]
        public int ColorInteriorId { get; set; }

        [JsonPropertyName("count_show")]
        public int CountShow { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("ocn")]
        public string Ocn { get; set; }

        [JsonPropertyName("count_available")]
        public int CountAvailable { get; set; }

        [JsonPropertyName("model_name")]
        public string ModelName { get; set; }

        [JsonPropertyName("discount_state")]
        public int DiscountState { get; set; }

        [JsonPropertyName("discount_start")]
        public int DiscountStart { get; set; }

        [JsonPropertyName("credit_available")]
        public int CreditAvailable { get; set; }

        [JsonPropertyName("cash_available")]
        public int CashAvailable { get; set; }
    }
}