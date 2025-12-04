using Microsoft.AspNetCore.Mvc;

public class Weatherforecast
{
    public DateTime Date { get;set; }
    public int TemperatureC { get;set; }
    public string? Summary { get;set; }
}
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
    };

    [HttpGet]
    public IEnumerable<Weatherforecast> Get()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select( index => new Weatherforecast
        {
            Date = DateTime.Now.AddDays (index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        }).ToArray();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Weatherforecast forcast)
    {
        // Add data to storage (e.g., database)
        return Ok(forcast);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Weatherforecast forcast)
    {
        // Update data for the given ID
	// Example: Find and update an item with a matching ID
	//    var existingForecast = fetch the data;
    //    existingForecast.Date = forcast.Date;
       return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
          // Delete data for the given ID
          return NoContent();
    }

}