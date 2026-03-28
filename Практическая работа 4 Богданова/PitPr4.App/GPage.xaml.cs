using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace PitPr4.App;

public partial class GPage : Page
{
    public GPage()
    {
        InitializeComponent();
    }

    private void BtnCalc_OnClick(object sender, RoutedEventArgs e)
    {
        if (!double.TryParse(TxtX.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
            || !double.TryParse(TxtY.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var y)
            || !double.TryParse(TxtZ.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var z))
        {
            TxtResult.Text = "Введите корректные числа для x, y и z.";
            return;
        }

        if (Variant14Math.TryComputeG(x, y, z, out var g, out var err))
            TxtResult.Text = $"g = {g.ToString(CultureInfo.InvariantCulture)}";
        else
            TxtResult.Text = err ?? "Ошибка вычисления.";
    }
}
