using Microsoft.AspNetCore.Mvc;
using OponyWeb.Connectors;
using OponyWeb.Dto;
using OponyWeb.Enums;

namespace OponyWeb.Controllers
{
    [ApiController]
    [Route("api")]
    public class TireStatusController : ControllerBase
    {
        private readonly IConfiguration _config;
        public TireStatusController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("location/{location}/tire-status")]
        public ActionResult<TireStatusOutputDto> GetTireStatus(string location)
        {
            const int temperatureThreshold = 7;

            var coordinates = new GeolocationConnector(_config).GetCoordinates(location);

            var daysBelow = 0;

            DateTime endDate = DateTime.Today;
            DateTime startDate = endDate.AddDays(-2);
            DateTime dtm = startDate;

            List<float> temps = new List<float>();
            while (dtm <= endDate)
            {
                var weather = new WeatherConnector(_config).GetWeather(coordinates, dtm);
                var averageTemperature = weather.GetAverageTemp();
                temps.Add(averageTemperature);

                if (averageTemperature < 7)
                {
                    daysBelow++;
                }

                dtm = dtm.AddDays(1);
            }

            float averageTemp = temps.Average();

            var winter = TireRecommendationResult.TireRecommendation.ChangeToWinter;
            var summer = TireRecommendationResult.TireRecommendation.ChangeToSummer;

            var recommendation = averageTemp < temperatureThreshold ?
                winter : summer;

            //var test = temps.Count(x => x < temperatureThreshold);

            float latitude = (float)coordinates.Lat;
            float longitude = (float)coordinates.Lon;

            return Ok(new TireStatusOutputDto()
            {
                DaysBelowTreshold = daysBelow,
                AverageTemperature = averageTemp,
                Recommendation = recommendation,
                Latitude = latitude,
                Longitude = longitude
            });
        }


    }
}
