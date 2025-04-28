namespace StatisticsApp.Core;

public class DescriptiveStatistics
{
    private readonly IEnumerable<double> _data;

    public DescriptiveStatistics(IEnumerable<double> data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        if (!data.Any())
            throw new ArgumentException("Data collection cannot be empty", nameof(data));
    }

    public double Mean => _data.Average();

    public double Median
    {
        get
        {
            var sorted = _data.OrderBy(x => x).ToList();
            int n = sorted.Count;
            if (n % 2 == 0)
                return (sorted[n / 2 - 1] + sorted[n / 2]) / 2;
            return sorted[n / 2];
        }
    }

    public double Variance
    {
        get
        {
            double mean = Mean;
            return _data.Average(x => Math.Pow(x - mean, 2));
        }
    }

    public double StandardDeviation => Math.Sqrt(Variance);

    public double Minimum => _data.Min();

    public double Maximum => _data.Max();

    public double Range => Maximum - Minimum;

    public double Mode
    {
        get
        {
            return _data.GroupBy(x => x)
                       .OrderByDescending(g => g.Count())
                       .ThenBy(g => g.Key)
                       .First()
                       .Key;
        }
    }

    public (double Q1, double Q2, double Q3) Quartiles
    {
        get
        {
            var sorted = _data.OrderBy(x => x).ToList();
            return (
                GetPercentile(sorted, 0.25),
                GetPercentile(sorted, 0.50),
                GetPercentile(sorted, 0.75)
            );
        }
    }

    private static double GetPercentile(List<double> sortedData, double percentile)
    {
        int n = sortedData.Count;
        double pos = (n + 1) * percentile;
        int intPos = (int)pos;
        double fraction = pos - intPos;

        if (intPos < 1) return sortedData[0];
        if (intPos >= n) return sortedData[n - 1];

        return sortedData[intPos - 1] + fraction * (sortedData[intPos] - sortedData[intPos - 1]);
    }
}
