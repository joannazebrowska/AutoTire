using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Validations;
using OponyWeb.Dto;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet("location/{location}/tire-status/{tireType}")]
        public ActionResult<TireStatusOutputDto> GetTireStatus(string location, string tireType)
        {

            DateTime endDate = DateTime.Today;
            DateTime startDate = endDate.AddDays(-2);
            DateTime dtm = startDate;

            List<float> temps = new List<float>();

            var coordinatesResponse = new WebClient().DownloadString($"http://api.openweathermap.org/geo/1.0/direct?q={location}&limit=1&appid={_config["apikey"]}");
            var coordinatesDocument = JsonDocument.Parse(coordinatesResponse);

            var lat = coordinatesDocument.RootElement[0].GetProperty("lat").ToString();
            var lon = coordinatesDocument.RootElement[0].GetProperty("lon").ToString();



            while (dtm <= endDate)
            {

                var date = dtm.ToString("yyyy-MM-dd");
                var response = new WebClient().DownloadString($"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={lat}&lon={lon}&date={date}&appid={_config["apikey"]}&units=metric");
                var document = JsonDocument.Parse(response);

                //var dateString = document.RootElement.GetProperty("date").ToString();

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

                dtm = dtm.AddDays(1);

                float averageTemperature = (minTemp + maxTemp) / 2;
                temps.Add(averageTemperature);

                System.Diagnostics.Debug.WriteLine(date);
                System.Diagnostics.Debug.WriteLine(averageTemperature);
            }

            float averageTemp = temps.Average();
            System.Diagnostics.Debug.WriteLine(averageTemp);

            string recommendation;

            if (averageTemp < 7 && tireType == "summer")
            {
                recommendation = "zmien opony na zimowe!";
            }
            else if (averageTemp >= 7 && tireType == "winter")
            {
                recommendation = "zmien opony na letnie!";
            }
            else
            {
                recommendation = "nie musisz zmieniac opon";
            }




            return Ok(new TireStatusOutputDto()
            {
                DaysBelowTreshold = 3,
                AverageTemperature = averageTemp,
                Recommendation = recommendation
            });
        }
    }
}
