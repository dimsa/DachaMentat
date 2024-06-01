using DachaMentat.Common;
using DachaMentat.Db;
using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class SensorDataDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coordinates")]
        public GeoCoordinatesDto Coordinates { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("indications")]
        public StoredIndicationDto[] Indications { get; set; }

    }
}
