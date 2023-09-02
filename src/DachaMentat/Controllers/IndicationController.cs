using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DachaMentat.Controllers
{
    [ApiController]
    public class IndicationController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IndicationService _indicationService;

        public IndicationController(ILogger<WeatherForecastController> logger, IndicationService sensorService)
        {
            _logger = logger;
            _indicationService = sensorService;
        }

        /// <summary>
        /// Gets the last indication time.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">Sensor {id} not found</exception>
        internal async Task<string> GetLastIndicationTime(int id)
        {
            using (var context = new MentatSensorsDbContext())
            {
                var sensorExists = context.Sensors.Any(it => it.Id == id);

                if (sensorExists)
                {
                    var sensor = await context.Sensors.FirstOrDefaultAsync(it => it.Id == id);

                    if (sensor == null)
                    {
                        throw new MentatDbException($"Sensor {id} not found");
                    }

                    var lastIndication = context.Indications.Where(it => it.SensorId == id).OrderByDescending(it => it.Timestamp).FirstOrDefault();

                    return $"{lastIndication.Value} {sensor.UnitOfMeasure} ({lastIndication.Timestamp.ToString("dd.MM.yyyy HH:mm:ss")})";
                }

                return null;
            }
        }

        [HttpGet("/Indication/{id}")]
        public async Task<string> GetLastIndicationTimestamp(int id)
        {
            return await _indicationService.GetLastIndicationTime(id);
        }
        
        [HttpPut("/Indication/{id}")]
        public Task<bool> AddIndication(int id, [FromBody] SensorIndicationDto indication)
        {
            return _indicationService.AddIndication(id, indication.PrivateKey, indication.Value);
        }

      
    }
}