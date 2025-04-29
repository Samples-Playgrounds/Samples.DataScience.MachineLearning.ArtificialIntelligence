using Statistics.Core;
using System.Globalization;

namespace Statistics.MauiApp;

public partial class MainPage : ContentPage
{
	private DescriptiveStatistics _stats;
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private void OnAnalyzeClicked(object sender, EventArgs e)
	{
		try
		{
			ErrorMessage.IsVisible = false;
			string input = DataInput.Text?.Trim();

			if (string.IsNullOrWhiteSpace(input))
			{
				ShowError("Please enter some data to analyze.");
				return;
			}

			var data = ParseInput(input);
			if (data.Length == 0)
			{
				ShowError("No valid numbers found in the input.");
				return;
			}

			// Create statistics instance and update UI with results
			_stats = new DescriptiveStatistics(data);
			DisplayResults();
		}
		catch (Exception ex)
		{
			ShowError($"Error: {ex.Message}");
		}
	}

	private void OnLoadSampleClicked(object sender, EventArgs e)
	{
		double[] sampleData = { 5, 19, 24, 62, 91, 100, 75, 42, 44, 16, 21, 34, 80, 52, 47, 6, 42, 18, 13, 23 };
		DataInput.Text = string.Join(", ", sampleData);
		OnAnalyzeClicked(sender, e);
	}

	private void OnClearClicked(object sender, EventArgs e)
	{
		DataInput.Text = string.Empty;
		ErrorMessage.IsVisible = false;
		ResultsPanel.IsVisible = false;
		_stats = null;
	}

	private double[] ParseInput(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return Array.Empty<double>();

		var separators = new[] { ' ', ',', '\n', '\r', '\t', ';' };
		var numbers = new List<double>();

		var tokens = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
		foreach (var token in tokens)
		{
			if (double.TryParse(token.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
			{
				numbers.Add(value);
			}
		}

		return numbers.ToArray();
	}

	private void ShowError(string message)
	{
		ErrorMessage.Text = message;
		ErrorMessage.IsVisible = true;
		ResultsPanel.IsVisible = false;
	}

	private void DisplayResults()
	{
		// Update UI with statistics results
		CountValue.Text = _stats.Count.ToString();
		MinValue.Text = _stats.Minimum.ToString("F4");
		MaxValue.Text = _stats.Maximum.ToString("F4");
		RangeValue.Text = _stats.Range.ToString("F4");
		MeanValue.Text = _stats.Mean.ToString("F4");
		MedianValue.Text = _stats.Median.ToString("F4");
		ModeValue.Text = _stats.Mode.ToString("F4");
		StdDevValue.Text = _stats.StandardDeviation.ToString("F4");
		VarianceValue.Text = _stats.Variance.ToString("F4");
		P25Value.Text = _stats.GetPercentile(25).ToString("F4");
		P75Value.Text = _stats.GetPercentile(75).ToString("F4");
		IQRValue.Text = (_stats.GetPercentile(75) - _stats.GetPercentile(25)).ToString("F4");

		ResultsPanel.IsVisible = true;
	}
}
