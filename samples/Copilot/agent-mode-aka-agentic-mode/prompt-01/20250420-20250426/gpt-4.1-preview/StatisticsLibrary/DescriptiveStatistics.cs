using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticsLibrary
{
    /// <summary>
    /// Provides methods for calculating descriptive statistics.
    /// </summary>
    public static class DescriptiveStatistics
    {
        /// <summary>
        /// Calculates the mean (average) of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers.</param>
        /// <returns>The mean of the collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the data collection is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the data collection is empty.</exception>
        public static double Mean(IEnumerable<double> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.Any()) throw new InvalidOperationException("Cannot calculate mean of an empty collection.");

            return data.Average();
        }

        /// <summary>
        /// Calculates the median of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers.</param>
        /// <returns>The median of the collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the data collection is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the data collection is empty.</exception>
        public static double Median(IEnumerable<double> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var sortedData = data.OrderBy(n => n).ToList();
            if (!sortedData.Any()) throw new InvalidOperationException("Cannot calculate median of an empty collection.");

            int count = sortedData.Count;
            int midIndex = count / 2;

            if (count % 2 == 0)
            {
                // Even number of elements: average the two middle elements
                return (sortedData[midIndex - 1] + sortedData[midIndex]) / 2.0;
            }
            else
            {
                // Odd number of elements: return the middle element
                return sortedData[midIndex];
            }
        }

        /// <summary>
        /// Calculates the mode(s) of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers.</param>
        /// <returns>A list containing the mode(s). Returns an empty list if the collection is empty or has no mode (all elements unique).</returns>
        /// <exception cref="ArgumentNullException">Thrown if the data collection is null.</exception>
        public static List<double> Mode(IEnumerable<double> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.Any()) return new List<double>();

            var frequencyMap = data.GroupBy(n => n)
                                   .ToDictionary(g => g.Key, g => g.Count());

            if (!frequencyMap.Any()) return new List<double>(); // Should not happen if data is not empty, but defensive check

            int maxFrequency = frequencyMap.Values.Max();

            // If all elements occur with the same frequency (and that frequency is 1), there is no mode.
            if (maxFrequency <= 1 && frequencyMap.Values.All(f => f == 1))
            {
                 return new List<double>();
            }

            return frequencyMap.Where(kvp => kvp.Value == maxFrequency)
                               .Select(kvp => kvp.Key)
                               .OrderBy(n => n) // Optional: Order modes for consistency
                               .ToList();
        }

        /// <summary>
        /// Calculates the range of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers.</param>
        /// <returns>The range (difference between max and min).</returns>
        /// <exception cref="ArgumentNullException">Thrown if the data collection is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the data collection is empty.</exception>
        public static double Range(IEnumerable<double> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.Any()) throw new InvalidOperationException("Cannot calculate range of an empty collection.");

            return data.Max() - data.Min();
        }

        /// <summary>
        /// Calculates the sample variance of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers (sample).</param>
        /// <returns>The sample variance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the data collection is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the data collection has fewer than 2 elements.</exception>
        public static double SampleVariance(IEnumerable<double> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            int n = data.Count();
            if (n < 2) throw new InvalidOperationException("Sample variance requires at least two data points.");

            double mean = Mean(data);
            double sumOfSquares = data.Sum(d => Math.Pow(d - mean, 2));

            return sumOfSquares / (n - 1);
        }

        /// <summary>
        /// Calculates the population variance of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers (population).</param>
        /// <returns>The population variance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the data collection is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the data collection is empty.</exception>
        public static double PopulationVariance(IEnumerable<double> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            int n = data.Count();
            if (n == 0) throw new InvalidOperationException("Cannot calculate population variance of an empty collection.");

            double mean = Mean(data);
            double sumOfSquares = data.Sum(d => Math.Pow(d - mean, 2));

            return sumOfSquares / n;
        }

        /// <summary>
        /// Calculates the sample standard deviation of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers (sample).</param>
        /// <returns>The sample standard deviation.</returns>
        public static double SampleStandardDeviation(IEnumerable<double> data)
        {
            return Math.Sqrt(SampleVariance(data));
        }

        /// <summary>
        /// Calculates the population standard deviation of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection of numbers (population).</param>
        /// <returns>The population standard deviation.</returns>
        public static double PopulationStandardDeviation(IEnumerable<double> data)
        {
            return Math.Sqrt(PopulationVariance(data));
        }
    }
}
