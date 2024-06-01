using Newtonsoft.Json;

namespace DachaMentat.DTO
{
    public class StoredIndicationDto
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }

    }
}
