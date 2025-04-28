using StatisticsApp.Core;

namespace StatisticsApp.Maui;

public partial class MainPage : ContentPage
{
    public string DataInput { get; set; } = string.Empty;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OnAnalyzeClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(DataInput))
            {
                ShowError("Please enter some numbers");
                return;
            }

            var numbers = DataInput.Split(',')
                                 .Select(s => double.Parse(s.Trim()))
                                 .ToList();

            if (!numbers.Any())
            {
                ShowError("Please enter valid numbers");
                return;
            }

            var stats = new DescriptiveStatistics(numbers);
            var quartiles = stats.Quartiles;

            // Update UI with results
            MeanLabel.Text = stats.Mean.ToString("F2");
            MedianLabel.Text = stats.Median.ToString("F2");
            ModeLabel.Text = stats.Mode.ToString("F2");
            StdDevLabel.Text = stats.StandardDeviation.ToString("F2");
            RangeLabel.Text = stats.Range.ToString("F2");
            Q1Label.Text = quartiles.Q1.ToString("F2");
            Q3Label.Text = quartiles.Q3.ToString("F2");

            // Show results and hide error
            ResultsGrid.IsVisible = true;
            ErrorLabel.IsVisible = false;
        }
        catch (Exception ex)
        {
            ShowError($"Error: {ex.Message}");
        }
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
        ResultsGrid.IsVisible = false;
    }
}
