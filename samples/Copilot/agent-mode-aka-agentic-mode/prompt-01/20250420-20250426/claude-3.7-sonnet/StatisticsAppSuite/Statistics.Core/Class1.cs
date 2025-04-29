using System;
using System.Linq;

namespace Statistics.Core;

/// <summary>
/// Provides core functionality for descriptive statistics calculations.
/// </summary>
public class DescriptiveStatistics
{
    private readonly double[] _data;

    /// <summary>
    /// Initializes a new instance of the DescriptiveStatistics class with the specified data set.
    /// </summary>
    /// <param name="data">The data set to analyze.</param>
    public DescriptiveStatistics(double[] data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        if (data.Length == 0)
        {
            throw new ArgumentException("Data set cannot be empty.", nameof(data));
        }
    }

    /// <summary>
    /// Gets the minimum value in the data set.
    /// </summary>
    public double Minimum => _data.Min();

    /// <summary>
    /// Gets the maximum value in the data set.
    /// </summary>
    public double Maximum => _data.Max();

    /// <summary>
    /// Gets the range of the data set (maximum - minimum).
    /// </summary>
    public double Range => Maximum - Minimum;

    /// <summary>
    /// Gets the mean (average) of the data set.
    /// </summary>
    public double Mean => _data.Average();

    /// <summary>
    /// Gets the median of the data set.
    /// </summary>
    public double Median
    {
        get
        {
            var sortedData = _data.OrderBy(x => x).ToArray();
            int n = sortedData.Length;
            if (n % 2 == 0)
            {
                return (sortedData[n / 2 - 1] + sortedData[n / 2]) / 2.0;
            }
            return sortedData[n / 2];
        }
    }

    /// <summary>
    /// Gets the mode of the data set (most frequently occurring value).
    /// </summary>
    public double Mode
    {
        get
        {
            return _data.GroupBy(x => x)
                        .OrderByDescending(g => g.Count())
                        .First()
                        .Key;
        }
    }

    /// <summary>
    /// Gets the variance of the data set.
    /// </summary>
    public double Variance
    {
        get
        {
            double mean = Mean;
            double sum = _data.Sum(x => Math.Pow(x - mean, 2));
            return sum / _data.Length;
        }
    }

    /// <summary>
    /// Gets the standard deviation of the data set.
    /// </summary>
    public double StandardDeviation => Math.Sqrt(Variance);

    /// <summary>
    /// Gets the sum of all values in the data set.
    /// </summary>
    public double Sum => _data.Sum();

    /// <summary>
    /// Gets the count of values in the data set.
    /// </summary>
    public int Count => _data.Length;

    /// <summary>
    /// Gets the specified percentile value from the data set.
    /// </summary>
    /// <param name="percentile">The percentile to calculate (0-100).</param>
    /// <returns>The value at the specified percentile.</returns>
    public double GetPercentile(double percentile)
    {
        if (percentile < 0 || percentile > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(percentile), "Percentile must be between 0 and 100.");
        }

        var sortedData = _data.OrderBy(x => x).ToArray();
        if (percentile == 0)
        {
            return sortedData[0];
        }
        else if (percentile == 100)
        {
            return sortedData[sortedData.Length - 1];
        }

        double index = (percentile / 100.0) * (sortedData.Length - 1);
        int lowerIndex = (int)Math.Floor(index);
        int upperIndex = (int)Math.Ceiling(index);

        if (lowerIndex == upperIndex)
        {
            return sortedData[lowerIndex];
        }

        double weight = index - lowerIndex;
        return sortedData[lowerIndex] * (1 - weight) + sortedData[upperIndex] * weight;
    }

    /// <summary>
    /// Gets the skewness of the data set.
    /// </summary>
    public double Skewness
    {
        get
        {
            double mean = Mean;
            double sd = StandardDeviation;
            double n = _data.Length;
            
            if (sd == 0)
            {
                return 0;
            }
            
            double sum = _data.Sum(x => Math.Pow((x - mean) / sd, 3));
            return n * sum / ((n - 1) * (n - 2));
        }
    }

    /// <summary>
    /// Gets the kurtosis of the data set.
    /// </summary>
    public double Kurtosis
    {
        get
        {
            double mean = Mean;
            double variance = Variance;
            double n = _data.Length;

            if (variance == 0)
            {
                return 0;
            }

            double sum = _data.Sum(x => Math.Pow(x - mean, 4));
            return (sum / (_data.Length * Math.Pow(variance, 2))) - 3;
        }
    }

    /// <summary>
    /// Creates a statistical summary with the most important descriptive statistics.
    /// </summary>
    /// <returns>A string containing a summary of the descriptive statistics.</returns>
    public string GetSummary()
    {
        return $"Descriptive Statistics Summary:\n" +
               $"Count: {Count}\n" +
               $"Minimum: {Minimum:F4}\n" +
               $"Maximum: {Maximum:F4}\n" +
               $"Range: {Range:F4}\n" +
               $"Mean: {Mean:F4}\n" +
               $"Median: {Median:F4}\n" +
               $"Mode: {Mode:F4}\n" +
               $"Standard Deviation: {StandardDeviation:F4}\n" +
               $"Variance: {Variance:F4}\n" +
               $"Skewness: {Skewness:F4}\n" +
               $"Kurtosis: {Kurtosis:F4}\n" +
               $"25th Percentile: {GetPercentile(25):F4}\n" +
               $"75th Percentile: {GetPercentile(75):F4}\n";
    }
}
