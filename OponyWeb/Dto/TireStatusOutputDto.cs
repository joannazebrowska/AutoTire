using OponyWeb.Enums;
namespace OponyWeb.Dto
{
    public class TireStatusOutputDto
    {
        public float AverageTemperature { get; set; }
        public int DaysBelowTreshold { get; set; }
        public TireRecommendationResult.TireRecommendation Recommendation { get; set; }
    }
}
