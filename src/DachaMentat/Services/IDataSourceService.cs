using DachaMentat.Db;

namespace DachaMentat.Services
{
    public interface IDataSourceService
    {
        public MentatSensorsDbContext GetDbContext();

        public MentatUsersDbContext GetUsersDbContext();
    }
}