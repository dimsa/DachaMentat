using DachaMentat.Common;
using DachaMentat.DTO;
using DachaMentat.Services;

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

        public async Task<bool> DeleteSensor(int id)
        {
            return await _sensorService.RemoveSensor(id);
        }

        public async Task<IEnumerable<SensorAdminDto>> GetAdminSensors()
        {
            return await _sensorService.GetAdminSensors();
        }

        public async Task<IEnumerable<SensorGuestDto>> GetGuestSensors()
        {
            return await _sensorService.GetSensorsView();
        }

        public async Task<SensorDataDto> GetSensorInfo(int id)
        {
            return await _sensorService.GetSensorInfo(id);
        }

        public async Task<bool> UpdateSensor(int id, SensorAdminDto updatedSensor)
        {
            return await _sensorService.UpdateSensor(
                id,
                updatedSensor.PrivateKey, 
                updatedSensor.Name, 
                updatedSensor.UnitOfMeasure, 
                GeoCoordinates.CreateFromDto(updatedSensor.Coordinates));
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
