using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class TokenResponse : BaseResponse
    {
        public TokenResponse(string token) : base() 
        {
            Token = token;
            ResponseStatus = new ResponseStatus() { IsSuccess = true, Message = "Token obtained" };
        }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
