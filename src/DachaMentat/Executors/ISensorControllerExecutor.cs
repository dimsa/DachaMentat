using DachaMentat.DTO;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentat.Executors
{
    public interface ISensorControllerExecutor
    {
        Task<IEnumerable<SensorViewDto>> GetSensors();

        //Task<SensorRegistrationResponseDto> RegisterSensor(SensorRegistrationDto sensorDto);

        Task<bool> AddNewSensor();
    }
}
