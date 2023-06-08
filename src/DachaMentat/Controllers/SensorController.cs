using DachaMentor.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentor.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SensorService _sensorService;

        public SensorController(ILogger<WeatherForecastController> logger, SensorService sensorService)
        {
            _logger = logger;
            _sensorService = sensorService;
        }

        [HttpGet(Name = "GetLastFull")]
        public IEnumerable<SensorData> GetLastFull()
        {
            var rnd = new Random();
            return new List<SensorData>()
            {
                new SensorData { Date= DateTime.Now, Id = "Test", Value = rnd.NextDouble() * 40 },
            };
        }

        [HttpPut(Name = "AddIndication/{id}/{value}")]
        public Task<bool> AddIndication(string id, double value)
        {
            return _sensorService.AddIndication(id, value);
        }
    }
}