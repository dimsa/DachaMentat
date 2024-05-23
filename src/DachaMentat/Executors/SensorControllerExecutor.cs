using DachaMentat.DTO;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentat.Executors
{
    public class SensorControllerExecutor : ISensorControllerExecutor
    {
        private readonly SensorService _sensorService;

        public SensorControllerExecutor(SensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public Task<bool> AddNewSensor()
        {
            return _sensorService.AddNewSensor();
        }

        public async Task<IEnumerable<SensorViewDto>> GetSensors()
        {
            return await _sensorService.GetSensorsView();
        }

       /* public async Task<SensorRegistrationResponseDto> RegisterSensor(SensorRegistrationDto sensorDto)
        {
            var registrationResult = await _sensorService.RegisterSensor(sensorDto.PrivateId, sensorDto.UnitOfMeasure, sensorDto.Coordinates);
            var result = new SensorRegistrationResponseDto()
            {
                Id = registrationResult.Item1,
                PrivateKey = registrationResult.Item2,
            };

            return result;

        }*/
    }
}
