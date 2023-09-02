using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Utils;
using Microsoft.EntityFrameworkCore;

namespace DachaMentor.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class IndicationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndicationService"/> class.
        /// </summary>
        public IndicationService()
        {

        }

        /// <summary>
        /// Adds the indication.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">
        /// Please use private key to add indication
        /// or
        /// Sensor was not found
        /// </exception>
        public async Task<bool> AddIndication(int id, string privateKey, double value)
        {
            using (var context = new MentatSensorsDbContext())
            {
                if (string.IsNullOrEmpty(privateKey))
                {
                    throw new MentatDbException("Please use private key to add indication");
                }

                var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == id && privateKey == s.PrivateKey);

                if (existingSensor == null)
                {
                    throw new MentatDbException("Sensor was not found");
                }

                var indication = new Indication
                {
                    SensorId = id,
                    Timestamp = DateTime.UtcNow.ToUniversalTime(),
                    Value = value,
                };

                context.Indications.Add(indication);
                await context.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Gets the indications.
        /// </summary>
        /// <param name="sensorId">The sensor identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the last indication time.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">Sensor {id} not found</exception>
        internal async Task<string> GetLastIndicationTime(int id)
        {
            using (var context = new MentatSensorsDbContext())
            {
                var sensorExists = context.Sensors.Any(it => it.Id == id);

                if (sensorExists)
                {
                    var sensor = await context.Sensors.FirstOrDefaultAsync(it => it.Id == id);

                    if (sensor == null)
                    {
                        throw new MentatDbException($"Sensor {id} not found");
                    }

                    var lastIndication = context.Indications.Where(it => it.SensorId == id).OrderByDescending(it => it.Timestamp).FirstOrDefault();

                    return $"{lastIndication.Value} {sensor.UnitOfMeasure} ({lastIndication.Timestamp.ToString("dd.MM.yyyy HH:mm:ss")})";
                }

                return null;
            }
        }
    }
}
