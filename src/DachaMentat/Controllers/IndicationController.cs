using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Services;
using DachaMentat.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DachaMentat.Controllers
{
    /// <summary>
    /// Indication Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class IndicationController : ControllerBase
    {
        private readonly ILogger<IndicationController> _logger;

        private readonly IndicationService _indicationService;

        private readonly SensorService _sensorService;

        public IndicationController(ILogger<IndicationController> logger, IndicationService indicationService, SensorService sensorService)
        {
            _logger = logger;
            _indicationService = indicationService;
            _sensorService = sensorService;
        }

        [HttpGet("/Indication/{id}")]
        public async Task<string> GetLastIndicationTimestamp(int id)
        {
            return await _indicationService.GetLastIndicationTime(id);
        }

        [HttpGet("/Indication/report/{id}")]
        public async Task<ChartIndicationDto> GetLastIndicationTimestamp(int id, string start, string end)
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

        [HttpPut("/Indication/{id}")]
        public Task<bool> AddIndication(int id, [FromBody] SensorIndicationDto indication)
        {
            return _indicationService.AddIndication(id, indication.PrivateKey, indication.Value);
        }
    }
}