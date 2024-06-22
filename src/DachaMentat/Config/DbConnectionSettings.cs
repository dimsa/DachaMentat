using DachaMentat.Db;

namespace DachaMentat.Config
{
    public class DbConnectionSettings
    {
        public DbType DatabaseType { get; set; }

        public string ConnectionString { get; set; }
    }
}
