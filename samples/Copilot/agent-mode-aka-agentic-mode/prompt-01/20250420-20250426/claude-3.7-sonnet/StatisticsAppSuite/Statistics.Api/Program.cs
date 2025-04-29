using Statistics.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Statistics API endpoints
app.MapPost("/statistics/analyze", (StatisticsRequest request) =>
{
    try
    {
        if (request.Data == null || request.Data.Length == 0)
        {
            return Results.BadRequest("Data array cannot be empty");
        }

        var stats = new DescriptiveStatistics(request.Data);
        return Results.Ok(new
        {
            Min = stats.Minimum,
            Max = stats.Maximum,
            Range = stats.Range,
            Mean = stats.Mean,
            Median = stats.Median,
            Mode = stats.Mode,
            StandardDeviation = stats.StandardDeviation,
            Variance = stats.Variance,
            Skewness = stats.Skewness,
            Kurtosis = stats.Kurtosis,
            Count = stats.Count,
            Sum = stats.Sum,
            Percentiles = new
            {
                P25 = stats.GetPercentile(25),
                P50 = stats.GetPercentile(50),
                P75 = stats.GetPercentile(75),
                P90 = stats.GetPercentile(90),
                P95 = stats.GetPercentile(95),
                P99 = stats.GetPercentile(99)
            }
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Error analyzing data: {ex.Message}");
    }
})
.WithName("AnalyzeStatistics")
.WithOpenApi();

// Sample endpoint with predefined data
app.MapGet("/statistics/sample", () =>
{
    double[] sampleData = { 5, 19, 24, 62, 91, 100, 75, 42, 44, 16, 21, 34, 80, 52, 47, 6, 42, 18, 13, 23 };
    var stats = new DescriptiveStatistics(sampleData);
    
    return new
    {
        Data = sampleData,
        Stats = new
        {
            Min = stats.Minimum,
            Max = stats.Maximum,
            Range = stats.Range,
            Mean = stats.Mean,
            Median = stats.Median,
            Mode = stats.Mode,
            StandardDeviation = stats.StandardDeviation,
            Variance = stats.Variance,
            Count = stats.Count,
            Sum = stats.Sum
        }
    };
})
.WithName("GetSampleStatistics")
.WithOpenApi();

app.Run();

// Request DTO model
record StatisticsRequest(double[] Data);

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
