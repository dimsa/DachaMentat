using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Services;
using DachaMentat.Utils;

namespace DachaMentat.Executors
{
    public class IndicationControllerExeсutor : IIndicationControllerExecutor
    {
        private IndicationService _indicationService;

        private SensorService _sensorService;

        public IndicationControllerExeсutor(IndicationService indicationService, SensorService sensorService)
        {
            _indicationService = indicationService;
            _sensorService = sensorService;
        }

        public Task<bool> AddIndication(int id, SensorObtainedIndicationDto indication)
        {
            return _indicationService.AddIndication(id, indication.PrivateKey, indication.Value);
        }

        public async Task<string> GetLastIndicationTimestamp(int id)
        {
            return await _indicationService.GetLastIndicationTime(id);
        }

        public async Task<ChartIndicationDto> GetDataForReport(int id, string start, string end)
        {
            var startDate = DateTimeHelper.ParseDate(start);
            var endDate = DateTimeHelper.ParseDate(end);

            if ((endDate - startDate) > TimeSpan.FromDays(365) || (endDate - startDate) < TimeSpan.Zero)
            {
                throw new MentatRestrictionException("Please select period less then 1 year");
            }

            var indicationsFromDb = await _indicationService.GetIndications(id, startDate, endDate);
            var unitOfMeasue = await _sensorService.GetSensorUnitOfMeasure(id);

            var result = DataFormatter.FormatIndicationDataForChart(indicationsFromDb);
            result.UnitOfMeasure = unitOfMeasue;

            return result;
        }
    }
}
