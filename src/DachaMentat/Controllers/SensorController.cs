using DachaMentat.DTO;
using DachaMentat.Executors;
using DachaMentat.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IEnumerable<SensorGuestDto>> GetSensors()
        {
            return await _executor.GetGuestSensors();
        }

        [HttpGet("config/sensors")]
        [Authorize(Roles = "Administrators")]
        public async Task<IEnumerable<SensorAdminDto>> GetAdminSensors()
        {
            return await _executor.GetAdminSensors();
        }

        [HttpGet("config/sensors/add")]
        [Authorize(Roles = "Administrators")]
        public async Task<bool> AddSensor()
        {
            try
            {
                await _executor.AddNewSensor();
                return true;
            }
            catch (Exception)
            {

                return false;
            }            
        }


        [HttpPost("config/sensor/{id}")]
        [Authorize(Roles = "Administrators")]
        public async Task<bool> UpdateSensors(int id, [FromBody] SensorAdminDto updatedSensor)
        {
            return await _executor.UpdateSensor(id, updatedSensor);
        }

        /*  [HttpPost("/sensors/register")]
          [Authorize(Roles = "Administrators")]
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

          }*/
    }
}