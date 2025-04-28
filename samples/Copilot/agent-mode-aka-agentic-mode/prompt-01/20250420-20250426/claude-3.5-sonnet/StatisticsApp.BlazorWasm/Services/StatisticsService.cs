using System.Net.Http.Json;
using StatisticsApp.Core;

namespace StatisticsApp.BlazorWasm.Services;

public class StatisticsService
{
    private readonly HttpClient _httpClient;

    public StatisticsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StatisticsResult?> AnalyzeDataAsync(IEnumerable<double> data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/statistics/analyze", data.ToArray());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StatisticsResult>();
            }
            throw new Exception($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to analyze data", ex);
        }
    }
}

public class StatisticsResult
{
    public double[] Data { get; set; } = Array.Empty<double>();
    public double Mean { get; set; }
    public double Median { get; set; }
    public double Mode { get; set; }
    public double StandardDeviation { get; set; }
    public double Variance { get; set; }
    public double Range { get; set; }
    public double Minimum { get; set; }
    public double Maximum { get; set; }
    public Quartiles Quartiles { get; set; } = new();
}

public class Quartiles
{
    public double Q1 { get; set; }
    public double Q2 { get; set; }
    public double Q3 { get; set; }
}