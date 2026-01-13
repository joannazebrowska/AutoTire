using System.Net;
using System.Text.Json;

namespace OponyWeb.Connectors
{
    public class WeatherConnector
    {
        private readonly IConfiguration _config;
        public WeatherConnector(IConfiguration config)
        {
            _config = config;
        }

        public WeatherData GetWeather(Coordinates coordinates, DateTime date)
        {
            var dateString = date.ToString("yyyy-MM-dd");

            var response = new WebClient().DownloadString($"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={coordinates.Lat}&lon={coordinates.Lon}&date={dateString}&appid={_config["OpenWeather"]}&units=metric");
            var document = JsonDocument.Parse(response);

            var minTempString = document.RootElement
                .GetProperty("temperature")
                .GetProperty("min")
                .ToString();
            var minTemp = float.Parse(minTempString);

            var maxTempString = document.RootElement
                .GetProperty("temperature")
                .GetProperty("max")
                .ToString();
            var maxTemp = float.Parse(maxTempString);

            return new WeatherData
            {
                Date = date,
                MinTemp = minTemp,
                MaxTemp = maxTemp
            };
        }
    }

    public class WeatherData
    {
        public DateTime Date { get; set; }
        public float MinTemp { get; set; }
        public float MaxTemp { get; set; }
        //public float AverageTemp => (MinTemp + MaxTemp) / 2;

        public float GetAverageTemp()
        {
            return (MinTemp + MaxTemp) / 2;
        }
    }
}
