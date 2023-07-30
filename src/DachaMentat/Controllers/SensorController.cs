using DachaMentat.DTO;
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

        [HttpPut(Name = "AddIndication/{id}")]
        public Task<bool> AddIndication(string id, [FromBody] SensorIndicationDto indication)
        {
            return _sensorService.AddIndication(id, indication.PrivateKey, indication.Value);
        }

        [HttpPost("RegisterSensor")]
        public IActionResult RegisterSensor([FromBody] SensorRegistrationDto sensorDto)
        {
            if (sensorDto == null)
            {
                return BadRequest("Invalid sensor data");
            }

            if (string.IsNullOrEmpty(sensorDto.PrivateId))
            {
                return BadRequest("PrivateId is required");
            }

            var registrationResult = _sensorService.RegisterSensor(sensorDto.PrivateId, sensorDto.UnitOfMeasure, sensorDto.Coordinates);

            var response = new SensorRegistrationResponseDto()
            {
                Id = registrationResult.Item1,
                PrivateKey = registrationResult.Item2,
            };
            return Ok(response);
        }
    }
}