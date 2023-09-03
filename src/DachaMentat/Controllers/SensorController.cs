using DachaMentat.DTO;
using DachaMentat.Executors;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentat.Controllers
{
    /// <summary>
    /// Sensor Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<SensorController> _logger;

        private readonly ISensorControllerExecutor _executor;

        public SensorController(ILogger<SensorController> logger, ISensorControllerExecutor executor)
        {
            _logger = logger;
            _executor = executor;
        }

        [HttpGet("/sensors")]
        public async Task<IEnumerable<string>> GetSensors()
        {
            return await _executor.GetSensors();
        }

        [HttpPost("/sensors/register")]
        public async Task<BaseResponse> RegisterSensor([FromBody] SensorRegistrationDto sensorDto)
        {
            if (sensorDto == null)
            {
                return new ErrorResponse("Sensor Data Can not Be null");
            }

            if (string.IsNullOrEmpty(sensorDto.PrivateId))
            {
                return new ErrorResponse("Private Key is Required");
            }

            var registrationResult =  await _executor.RegisterSensor(sensorDto);

            return registrationResult;

        }
    }
}