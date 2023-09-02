using DachaMentat.Db;
using DachaMentat.DTO;

namespace DachaMentat.Utils
{
    /// <summary>
    /// Data Formatter for different needs
    /// </summary>
    public class DataFormatter
    {
        public static ChartIndicationDto FormatIndicationDataForChart(IEnumerable<Indication> rawDbData)
        {            
            var dates = new List<DateTime>();
            var values = new List<double>();            

            foreach (var row in rawDbData)
            {
                dates.Add(row.Timestamp);
                values.Add(row.Value);
            }

            var indicationDto = new ChartIndicationDto() { DateTimes = dates.Select(it=> it.ToString("yyyy-MM-dd hh:mm:ss")).ToArray(), Values = values.ToArray() };

            return indicationDto;
        }
    }
}
