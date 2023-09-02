using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class ChartIndicationDto : BaseResponse
    {
        [JsonProperty("values")]
        public double[] Values { get; set; }

        [JsonProperty("datetimes")]
        public string[] DateTimes { get; set; }

        [JsonProperty("unitoofmeasure")]
        public string UnitOfMeasure { get; set; }
    }
}
