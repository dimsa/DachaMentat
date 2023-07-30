using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Utils;
using Microsoft.EntityFrameworkCore;

namespace DachaMentor.Services
{
    public class IndicationService
    {
        public IndicationService()
        {

        }

        public List<Indication> GetIndications(int sensorId, DateTime startDate, DateTime endDate)
        {
            CheckSensorExists(sensorId);

            using (var context = new MentatSensorsDbContext())
            {
                return context.Indications
                    .Where(i => i.SensorId == sensorId && i.Timestamp >= startDate && i.Timestamp <= endDate)
                    .OrderByDescending(i => i.Timestamp)
                    .ToList();
            }
        }

        public async Task<bool> AddIndication(int sensorId, string sensorPrivateKey, double value)
        {
            try
            {

                CheckSensorExistsAndCanBeAccessed(sensorId, sensorPrivateKey);

                using (var context = new MentatSensorsDbContext())
                {
                    var indication = new Indication()
                    {
                        Timestamp = DateTime.Now.ToUniversalTime(),
                        SensorId = sensorId,
                        Value = value
                    };

                    context.Indications.Add(indication);

                    context.SaveChanges();
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void CheckSensorExistsAndCanBeAccessed(int sensorId, string sensorPrivateKey)
        {
            using (var context = new MentatSensorsDbContext())
            {
                var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == sensorId);

                if (existingSensor == null || existingSensor.PrivateKey != sensorPrivateKey || string.IsNullOrEmpty(sensorPrivateKey))
                {
                    throw new MentatDbException("Sensor can not be found");
                }
            }
        }

        private static void CheckSensorExists(int sensorId)
        {
            using (var context = new MentatSensorsDbContext())
            {
                var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == sensorId);

                if (existingSensor == null)
                {
                    throw new MentatDbException("Sensor can not be found");
                }
            }
        }
    }
}
