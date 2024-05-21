using DachaMentat.DTO;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentat.Executors
{
    public interface IAdminControllerExecutor
    {
        Task<IEnumerable<string>> GetRegistrationKeys();

        Task<string> AddRegistrationKeys();

        Task<bool> FirstInit(string login, string password);
    }
}
