namespace OponyWeb
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Test
    {
        int amount;

        void SetAmount(int value)
        {
            amount = value;
        }

        int GetAmount()
        {
            return amount;
        }
    }
}
