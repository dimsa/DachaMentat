using Newtonsoft.Json;
using System.ComponentModel.Design.Serialization;
using System.Text.Json.Serialization;

namespace DachaMentat.DTO
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GeoCoordinatesDto
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }


        [JsonProperty("longitude")]
        public double Longitude { get; set; }


    }
}
