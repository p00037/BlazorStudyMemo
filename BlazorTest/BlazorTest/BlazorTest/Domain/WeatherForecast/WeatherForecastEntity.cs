namespace BlazorTest.Domain.WeatherForecast
{
    public class WeatherForecastEntity
    {
        public int? Id { get; set; }
        public string TenantId { get; set; } = default!;
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
