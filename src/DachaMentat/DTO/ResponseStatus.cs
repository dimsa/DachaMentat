using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class ResponseStatus
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
