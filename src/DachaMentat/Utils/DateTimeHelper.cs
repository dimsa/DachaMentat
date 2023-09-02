using System.Globalization;

namespace DachaMentat.Utils
{
    public class DateTimeHelper
    {
        // Format of string 2022-03-21
        // "yyyy-MM-dd";
        public static DateTime ParseDate(string date)
        {
            string format = "yyyy-MM-dd";

            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

    }
}
