using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class ErrorResponse : BaseResponse
    {
        public ErrorResponse(string message)
        {
            ResponseStatus = new ResponseStatus()
            {
                IsSuccess = false,
                Message = message
            };
        }        
    }
}
