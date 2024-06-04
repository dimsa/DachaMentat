using DachaMentat.Db;

namespace DachaMentat.Services
{
    public class DataSourceService : IDataSourceService
    {
        private MentatSensorsDbContext _sensorDbContext;
        private MentatUsersDbContext _usersDbContext;

        //  private string _connectionString;

        public DataSourceService(MentatSensorsDbContext sensorsDbContext, MentatUsersDbContext usersDbContext)
        {
            //_connectionString = connectionString;
            _sensorDbContext = sensorsDbContext;
            _usersDbContext = usersDbContext;
        }

        public MentatSensorsDbContext GetDbContext()
        {
            return _sensorDbContext;//  new MentatSensorsDbContext(_connectionString);
        }

        public MentatUsersDbContext GetUsersDbContext()
        {
            return _usersDbContext;// new MentatUsersDbContext(_connectionString);
        }
    }
}
