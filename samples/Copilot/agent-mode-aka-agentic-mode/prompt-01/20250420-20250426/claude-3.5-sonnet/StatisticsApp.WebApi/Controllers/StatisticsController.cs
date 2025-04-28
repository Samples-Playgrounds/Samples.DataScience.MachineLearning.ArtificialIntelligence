using Microsoft.AspNetCore.Mvc;
using StatisticsApp.Core;

namespace StatisticsApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    [HttpPost("analyze")]
    public IActionResult AnalyzeData([FromBody] double[] data)
    {
        if (data == null || !data.Any())
            return BadRequest("Data array cannot be empty");

        try
        {
            var stats = new DescriptiveStatistics(data);
            var quartiles = stats.Quartiles;

            return Ok(new
            {
                data = data,
                mean = stats.Mean,
                median = stats.Median,
                mode = stats.Mode,
                standardDeviation = stats.StandardDeviation,
                variance = stats.Variance,
                range = stats.Range,
                minimum = stats.Minimum,
                maximum = stats.Maximum,
                quartiles = new
                {
                    q1 = quartiles.Q1,
                    q2 = quartiles.Q2,
                    q3 = quartiles.Q3
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error analyzing data: {ex.Message}");
        }
    }
}