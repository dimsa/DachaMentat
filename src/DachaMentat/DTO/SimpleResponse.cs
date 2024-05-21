namespace DachaMentat.DTO
{
    public class SimpleResponse : BaseResponse
    {
        public SimpleResponse(string text) : base() 
        {
            Content = text;
        }

        public string Content { get; }
    }
}
