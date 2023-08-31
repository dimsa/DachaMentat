using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DachaMentor.Services
{
    public class SensorService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SensorService"/> class.
        /// </summary>
        public SensorService()
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
        /// Registers the sensor.
        /// </summary>
        /// <param name="privateId">The private identifier.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns></returns>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">
        /// Sensor can not be registered without privateId
        /// or
        /// Sensor must have unit of measure
        /// or
        /// Sensor was registered earlier. Please use UpdateSensor
        /// </exception>
        internal Tuple<int, string> RegisterSensor(string privateId, string unitOfMeasure, GeoCoordinates coordinates)
        {
            using (var context = new MentatSensorsDbContext())
            {
                if (string.IsNullOrEmpty(privateId))
                {
                    throw new MentatDbException("Sensor can not be registered without privateId");
                }

                if (string.IsNullOrEmpty(unitOfMeasure))
                {
                    throw new MentatDbException("Sensor must have unit of measure");
                }

                var existingSensor = context.Sensors.FirstOrDefault(s => s.PrivateId == privateId);

                if (existingSensor != null)
                {
                    throw new MentatDbException("Sensor was registered earlier. Please use UpdateSensor");
                }

                var newSensor = new Sensor
                {
                    PrivateId = privateId,
                    PrivateKey = Guid.NewGuid().ToString(),
                    UnitOfMeasure = unitOfMeasure,
                    GeoCoordinates = GeoCoordinatesSerializer.Serialize(coordinates)
                };

                context.Sensors.Add(newSensor);
                context.SaveChanges();

                return new Tuple<int, string>(newSensor.Id, newSensor.PrivateKey);
            }
        }

        /// <summary>
        /// Updates the sensor.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns></returns>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">Invalid Sensor Data</exception>
        internal bool UpdateSensor(int id, string privateKey, string unitOfMeasure, GeoCoordinates coordinates)
        {
            try
            {
                using (var context = new MentatSensorsDbContext())
                {
                    var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == id);

                    if (existingSensor == null || existingSensor.PrivateKey != privateKey || string.IsNullOrEmpty(privateKey))
                    {
                        throw new MentatDbException("Invalid Sensor Data");
                    }

                    existingSensor.UnitOfMeasure = unitOfMeasure;
                    existingSensor.GeoCoordinates = GeoCoordinatesSerializer.Serialize(coordinates);

                    context.SaveChanges();
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the sensors.
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<string>> GetSensors()
        {
            using (var context = new MentatSensorsDbContext())
            {
                var existingSensors = await context.Sensors.Select(it => GetSensorRow(it)).ToArrayAsync();
                return existingSensors;
            }
        }

        /// <summary>
        /// Gets the sensor row.
        /// </summary>
        /// <param name="it">It.</param>
        /// <returns></returns>
        private static string GetSensorRow(Sensor it)
        {
             var res = $"{it.Id} {it.UnitOfMeasure}";
#if DEBUG
             res += " " + it.PrivateKey;
#endif
             return res; 
        }

        /// <summary>
        /// Removes the sensor.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="privateKey">The private key.</param>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">Invalid Sensor Data</exception>
        internal void RemoveSensor(int id, string privateKey)
        {
            using (var context = new MentatSensorsDbContext())
            {
                var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == id);

                if (existingSensor == null || existingSensor.PrivateKey != privateKey || string.IsNullOrEmpty(privateKey))
                {
                    throw new MentatDbException("Invalid Sensor Data");
                }

                context.Sensors.Remove(existingSensor);

                context.SaveChanges();
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
