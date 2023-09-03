using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Executors;
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

        private readonly IIndicationControllerExecutor _executor;

        public IndicationController(ILogger<IndicationController> logger, IIndicationControllerExecutor executor)
        {
            _logger = logger;
            _executor = executor;
        }

        [HttpGet("/Indication/{id}")]
        public async Task<string> GetLastIndicationTimestamp(int id)
        {
            return await _executor.GetLastIndicationTimestamp(id);
        }

        [HttpGet("/Indication/report/{id}")]
        public async Task<ChartIndicationDto> GetDataForReport(int id, string start, string end)
        {
            return await _executor.GetDataForReport(id, start, end);
        }

        [HttpPut("/Indication/{id}")]
        public Task<bool> AddIndication(int id, [FromBody] SensorIndicationDto indication)
        {
            return _executor.AddIndication(id, indication);
        }
    }
}