using System.Net;
using System.Text.Json;

namespace OponyWeb.Connectors
{
    public class GeolocationConnector
    {
        private readonly IConfiguration _config;
        public GeolocationConnector(IConfiguration config)
        {
            _config = config;
        }

        public Coordinates GetCoordinates(string location)
        {
            var coordinatesResponse = new WebClient().DownloadString($"http://api.openweathermap.org/geo/1.0/direct?q={location}&limit=1&appid={_config["OpenWeather"]}");
            var coordinatesDocument = JsonDocument.Parse(coordinatesResponse);

            var lat = coordinatesDocument.RootElement[0].GetProperty("lat").GetDecimal();
            var lon = coordinatesDocument.RootElement[0].GetProperty("lon").GetDecimal();

            return new Coordinates
            {
                Lat = lat,
                Lon = lon
            };
        }
    }

    public class Coordinates
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
    }
}
