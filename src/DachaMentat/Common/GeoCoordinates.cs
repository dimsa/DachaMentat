using DachaMentat.DTO;
using Newtonsoft.Json;

namespace DachaMentat.Common
{
    public class GeoCoordinates
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public GeoCoordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static GeoCoordinates CreateFromSting(string coordinates)
        {
            var textCoord = coordinates.Split(' ');

            var latitude = double.Parse(textCoord[0].Trim());
            var longitude = double.Parse(textCoord[1].Trim());

            return new GeoCoordinates(latitude, longitude);
        }

        public static GeoCoordinates CreateFromDto(GeoCoordinatesDto coordinates)
        {
            return new GeoCoordinates(coordinates.Latitude, coordinates.Longitude);
        }

        public GeoCoordinatesDto ToDto()
        {
            return new GeoCoordinatesDto() { Latitude = Latitude, Longitude = Longitude };
        }

        public override string ToString()
        {
            return string.Format("{0:0.000000}", Latitude) + " " + string.Format("{0:0.000000}", Longitude);
        }
    }
}
