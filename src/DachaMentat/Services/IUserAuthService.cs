namespace DachaMentat.Services
{
    public interface IUserAuthService
    {
        string CreateToken(string login, string password);
    }
}
