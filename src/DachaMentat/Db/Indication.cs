namespace DachaMentat.Db
{
    public class Indication
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
