using DachaMentat.Common;
using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class SensorGuestDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coordinates")]
        public GeoCoordinatesDto Coordinates { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("lastIndication")]
        public string LastIndication { get; set; }

        [JsonProperty("lastIndicationTimeStamp")]
        public string LastIndicationTimeStamp { get; set; }
    }
}
