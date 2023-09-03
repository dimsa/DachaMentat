using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class BaseResponse
    {
        [JsonProperty("responseStatus")]
        public ResponseStatus ResponseStatus { get; set; }        
    }
}
