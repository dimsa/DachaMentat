﻿namespace DachaMentat.Db
{
    public class Sensor
    {
        // Generated Id of sensor
        public int Id { get; set; }

        // Internal Id of sensor. 
        public string PrivateId { get; set; }   

        // Private Key of sensor for the uploading of indications and updating of Sensor data
        public string PrivateKey { get; set; }

        // Unit of measure of sensor
        public string UnitOfMeasure { get; set; }

        // Coordinates in JSON format
        public string GeoCoordinates { get; set; }
    }
}
