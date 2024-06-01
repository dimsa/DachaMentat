using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Services;
using DachaMentat.Utils;

namespace DachaMentat.Executors
{
    public interface IIndicationControllerExecutor 
    {
        Task<string> GetLastIndicationTimestamp(int id);

        Task<ChartIndicationDto> GetDataForReport(int id, string start, string end);

        Task<bool> AddIndication(int id, SensorObtainedIndicationDto indication);
    }
}
