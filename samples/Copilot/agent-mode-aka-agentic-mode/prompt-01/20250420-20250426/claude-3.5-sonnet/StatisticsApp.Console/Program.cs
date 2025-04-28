using StatisticsApp.Core;

// Sample dataset
var data = new double[] { 2.4, 4.1, 3.5, 7.8, 9.2, 4.1, 2.4, 6.5, 3.3, 2.9 };

// Create instance of DescriptiveStatistics
var stats = new DescriptiveStatistics(data);

Console.WriteLine("Descriptive Statistics Demo");
Console.WriteLine("=========================");
Console.WriteLine($"Dataset: {string.Join(", ", data)}");
Console.WriteLine();
Console.WriteLine($"Mean: {stats.Mean:F2}");
Console.WriteLine($"Median: {stats.Median:F2}");
Console.WriteLine($"Mode: {stats.Mode:F2}");
Console.WriteLine($"Standard Deviation: {stats.StandardDeviation:F2}");
Console.WriteLine($"Variance: {stats.Variance:F2}");
Console.WriteLine($"Range: {stats.Range:F2}");
Console.WriteLine($"Minimum: {stats.Minimum:F2}");
Console.WriteLine($"Maximum: {stats.Maximum:F2}");

var quartiles = stats.Quartiles;
Console.WriteLine();
Console.WriteLine("Quartiles:");
Console.WriteLine($"Q1 (25th percentile): {quartiles.Q1:F2}");
Console.WriteLine($"Q2 (50th percentile): {quartiles.Q2:F2}");
Console.WriteLine($"Q3 (75th percentile): {quartiles.Q3:F2}");
