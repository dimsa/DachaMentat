using DachaMentat.DTO;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentat.Controllers
{
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly SensorService _sensorService;

        public SensorController(ILogger<WeatherForecastController> logger, SensorService sensorService)
        {
            _logger = logger;
            _sensorService = sensorService;
        }

        [HttpGet("/sensors")]
        public async Task<IEnumerable<string>> GetSensors()
        {
            return await _sensorService.GetSensors();
        }

        [HttpPost("/sensors/register")]
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