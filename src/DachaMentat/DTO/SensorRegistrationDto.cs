using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class SensorRegistrationDto
    {
        [JsonProperty("privateId")]
        public string PrivateId { get; set; }

        [JsonProperty("coordinates")]
        public GeoCoordinatesDto Coordinates { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }
    }
}
