using OponyWeb.Enums;
using OponyWeb.Connectors;
namespace OponyWeb.Dto
{
    public class TireStatusOutputDto
    {
        public float AverageTemperature { get; set; }
        public int DaysBelowTreshold { get; set; }
        public TireRecommendationResult.TireRecommendation Recommendation { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
