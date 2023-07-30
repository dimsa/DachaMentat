using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class BaseResponse
    {
        [JsonProperty("responseStatus")]
        ResponseStatus ResponseStatus { get; set; }        
    }
}
