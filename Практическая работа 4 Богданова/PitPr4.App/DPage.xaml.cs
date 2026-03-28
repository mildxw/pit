using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace PitPr4.App;

public partial class DPage : Page
{
    public DPage()
    {
        InitializeComponent();
    }

    private void BtnCalc_OnClick(object sender, RoutedEventArgs e)
    {
        if (!double.TryParse(TxtX.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
            || !double.TryParse(TxtY.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var y))
        {
            TxtResult.Text = "Введите корректные числа для x и y.";
            return;
        }

        if (CmbF.SelectedItem is not ComboBoxItem item || item.Tag is not string tag)
        {
            TxtResult.Text = "Выберите f(x).";
            return;
        }

        var kind = tag switch
        {
            "Sh" => Variant14Math.FKind.Sh,
            "Square" => Variant14Math.FKind.Square,
            "Exp" => Variant14Math.FKind.Exp,
            _ => Variant14Math.FKind.Sh,
        };
        double d = Variant14Math.ComputeD(x, y, kind);
        string branch = x > y ? "x > y" : y > x ? "y > x" : "y = x";
        TxtResult.Text = $"Ветка: {branch}. d = {d.ToString(CultureInfo.InvariantCulture)}";
    }
}
