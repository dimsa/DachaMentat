﻿using DachaMentat.Common;
using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class SensorAdminDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coordinates")]
        public GeoCoordinatesDto Coordinates { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }

    }
}
