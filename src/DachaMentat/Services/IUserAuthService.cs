namespace DachaMentat.Services
{
    public interface IUserAuthService
    {
        string CreateToken(string login, string password);

        bool Setup(string login, string password);
    }
}
