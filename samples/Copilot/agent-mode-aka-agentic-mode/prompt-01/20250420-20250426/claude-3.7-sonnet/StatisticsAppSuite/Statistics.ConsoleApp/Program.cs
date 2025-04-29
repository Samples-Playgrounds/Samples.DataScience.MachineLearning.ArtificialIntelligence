// See https://aka.ms/new-console-template for more information
using Statistics.Core;

Console.WriteLine("Descriptive Statistics Console Application");
Console.WriteLine("=========================================");

// Example dataset for demonstration
double[] sampleData = { 5, 19, 24, 62, 91, 100, 75, 42, 44, 16, 21, 34, 80, 52, 47, 6, 42, 18, 13, 23 };

Console.WriteLine($"Sample data: {string.Join(", ", sampleData)}");
Console.WriteLine();

// Create an instance of our DescriptiveStatistics class
var stats = new DescriptiveStatistics(sampleData);

// Display the statistical summary
Console.WriteLine(stats.GetSummary());

// Example of using individual statistics properties
Console.WriteLine("\nAdditional statistics information:");
Console.WriteLine($"Interquartile Range (IQR): {stats.GetPercentile(75) - stats.GetPercentile(25):F4}");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
