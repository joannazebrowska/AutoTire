using OponyWeb.Controllers;

namespace OponyWeb.Dto
{
    public class TireStatusOutputDto
    {
        public float AverageTemperature { get; set; }
        public int DaysBelowTreshold { get; set; }
        public TireRecommendation Recommendation { get; set; }
    }
}
