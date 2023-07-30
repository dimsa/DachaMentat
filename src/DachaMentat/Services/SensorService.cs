using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Utils;
using Microsoft.EntityFrameworkCore;

namespace DachaMentor.Services
{
    public class SensorService
    {
        public SensorService()
        {

        }

        public async Task<bool> AddIndication(string id, string privateKey, double value)
        {
            await Task.Delay(500);
            return await Task.FromResult(true);
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
    }
}
