using DachaMentat.Common;
using DachaMentat.Db;
using DachaMentat.DTO;
using DachaMentat.Exceptions;
using DachaMentat.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace DachaMentat.Services
{
    public class SensorService
    {
        /// <summary>
        /// The data source
        /// </summary>
        private IDataSourceService _dataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorService"/> class.
        /// </summary>
        public SensorService(IDataSourceService dataSource) 
        {
            _dataSource = dataSource;
        }           

        /// <summary>
        /// Registers the sensor.
        /// </summary>
        /// <param name="name">The private identifier.</param>
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
        internal async Task<Tuple<int, string>> RegisterSensor(string name, string unitOfMeasure, GeoCoordinates coordinates)
        {
            using (var context = _dataSource.GetDbContext())
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new MentatDbException("Sensor can not be registered without name");
                }

                if (string.IsNullOrEmpty(unitOfMeasure))
                {
                    throw new MentatDbException("Sensor must have unit of measure");
                }

                /*var existingSensor = context.Sensors.FirstOrDefault(s => s.Name == name);

                if (existingSensor != null)
                {
                    throw new MentatDbException("Sensor was registered earlier. Please use UpdateSensor");
                }*/

                var newSensor = new Sensor
                {
                    Name = name,
                    PrivateKey = Guid.NewGuid().ToString(),
                    UnitOfMeasure = unitOfMeasure,
                    GeoCoordinates = coordinates.ToString()// GeoCoordinatesSerializer.Serialize(coordinates)
                };

                context.Sensors.Add(newSensor);
                var changesSaved = await context.SaveChangesAsync();

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
                using (var context = _dataSource.GetDbContext())
                {
                    var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == id);

                    if (existingSensor == null || existingSensor.PrivateKey != privateKey || string.IsNullOrEmpty(privateKey))
                    {
                        throw new MentatDbException("Invalid Sensor Data");
                    }

                    existingSensor.UnitOfMeasure = unitOfMeasure;
                    existingSensor.GeoCoordinates = coordinates.ToString();//GeoCoordinatesSerializer.Serialize(coordinates);

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
        public async Task<IEnumerable<SensorViewDto>> GetSensorsView()
        {
            using (var context = _dataSource.GetDbContext())
            {
                var a = context.Sensors.ToArray();

                var existingSensors = context.Sensors.
                    Join(
                        context.Indications,
                        s => s.Id,
                        i => i.SensorId,
                        (s, i) => new SensorViewDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            UnitOfMeasure = s.UnitOfMeasure,
                            Coordinates = GeoCoordinates.CreateFromSting(s.GeoCoordinates).ToDto(),
                            LastIndication = i.Value.ToString(),
                            LastIndicationTimeStamp = new KnownTimeStamp(i.Timestamp).ToString(),
                        });


                return existingSensors.ToArray();
            }
        }

        /// <summary>
        /// Gets the sensor unit of measure.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="DachaMentat.Exceptions.MentatDbException">Sensor with the specified ID wasn't found</exception>
        public async Task<string> GetSensorUnitOfMeasure(int id)
        {
            using (var context = _dataSource.GetDbContext())
            {
                var sensorData = await context.Sensors
                    .Where(s => s.Id == id).FirstOrDefaultAsync();  
                
                if (sensorData == null)
                {
                    throw new MentatDbException("Sensor with the specified ID wasn't found");
                }
                
                return sensorData.UnitOfMeasure;
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
        internal void RemoveSensor(int id)
        {
            using (var context = _dataSource.GetDbContext())
            {
                var existingSensor = context.Sensors.FirstOrDefault(s => s.Id == id);

                if (existingSensor == null/* || existingSensor.PrivateKey != privateKey || string.IsNullOrEmpty(privateKey)*/)
                {
                    throw new MentatDbException("Invalid Sensor Data");
                }

                context.Sensors.Remove(existingSensor);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds the new sensor with empty data
        /// </summary>
        /// <returns></returns>
        internal async Task<bool> AddNewSensor()
        {
            var res = await RegisterSensor("New Sensor", "UnitOfMeasure", new GeoCoordinates(0, 0));
            if (res != null && !string.IsNullOrEmpty(res.Item2))
            {
                return true;
            }

            return false;
        }
    }
}
