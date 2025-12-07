using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ErrorHandlingController : ControllerBase
{
    [HttpGet("division")]
    public IActionResult GetDivisionResult (int numerator, int denominator)
    {
        try
        {
            var result = numerator/denominator;
            return Ok($"Here is the result: {result}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine($"Error: Division by Zero not allowed");
            return BadRequest("Cannot devide by zero");

        }
    }
}