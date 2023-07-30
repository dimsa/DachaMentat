using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class SensorRegistrationResponseDto : BaseResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }
    }
}
