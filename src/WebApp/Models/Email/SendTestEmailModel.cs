using System.Text.Json.Serialization;

namespace WebApp.Models.Email
{
    public class SendTestEmailModel
    {
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }
    }
}