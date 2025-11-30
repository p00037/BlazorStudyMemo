using System;
using System.Collections.Generic;
using BlazorTest.Client.PageModels;
using BlazorTest.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTest.Controllers
{
    [ApiController]
    [Route("[controller]")] // ルートは /WeatherForecast になります
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public WeatherForecastController(ApplicationDbContext db) 
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<WeatherForecastPageEntitiy> Get()
        {
            IEnumerable<WeatherForecastPageEntitiy> pageEntities = _db.WeatherForecasts.Select(x => new WeatherForecastPageEntitiy
            {
                Date = x.Date,
                Summary = x.Summary,
                TemperatureC = x.TemperatureC
            }).ToArray();

            return pageEntities;
        }
    }
}
