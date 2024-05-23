namespace DachaMentat.Common
{
    public class KnownTimeStamp
    {
        private DateTime _dateTime;

        public KnownTimeStamp(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public KnownTimeStamp()
        {
            _dateTime = DateTime.Now;
        }

        public override string ToString()
        {
            return _dateTime.ToString("yyyy.MM.dd H:mm:ss");
        }
    }
}
