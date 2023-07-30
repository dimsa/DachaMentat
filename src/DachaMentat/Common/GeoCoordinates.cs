using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class GeoCoordinates
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }


        [JsonProperty("longitude")]
        public double Longitude { get; set; }


    }
}
