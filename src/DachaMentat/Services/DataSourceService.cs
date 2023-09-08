using DachaMentat.Db;

namespace DachaMentat.Services
{
    public class DataSourceService : IDataSourceService
    {
        private string _connectionString;

        public DataSourceService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MentatSensorsDbContext GetDbContext()
        {
            return new MentatSensorsDbContext(_connectionString);
        }
    }
}
