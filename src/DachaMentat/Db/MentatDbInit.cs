using Microsoft.EntityFrameworkCore;

namespace DachaMentat.Db
{
    public delegate void DbInitDelegate(DbContextOptionsBuilder optionsBuilder);
}
