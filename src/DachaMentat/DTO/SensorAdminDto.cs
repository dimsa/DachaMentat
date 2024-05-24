using DachaMentat.Common;
using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class SensorAdminDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coordinates")]
      //  РАБОТАЕТ ТОЛЬКО МЕЛКИМИ БУКВАМИ
        public GeoCoordinatesDto coordinates { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }

    }
}
