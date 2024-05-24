using DachaMentat.DTO;
using DachaMentat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DachaMentat.Executors
{
    public interface ISensorControllerExecutor
    {
        Task<IEnumerable<SensorGuestDto>> GetGuestSensors();

        Task<IEnumerable<SensorAdminDto>> GetAdminSensors();

        //Task<SensorRegistrationResponseDto> RegisterSensor(SensorRegistrationDto sensorDto);

        Task<bool> AddNewSensor();

        Task<bool> DeleteSensor(int id);

        Task<bool> UpdateSensor(int id, SensorAdminDto updatedSensor);
    }
}
