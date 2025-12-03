namespace BlazorTest.Domain.Weather
{
    public class WeatherForecastEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; } = default!;
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
