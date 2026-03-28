using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PitPr4.App;

public partial class PlotPage : Page
{
    private IReadOnlyList<(double X, double Y)>? _lastPts;

    public PlotPage()
    {
        InitializeComponent();
    }

    private sealed class PointVm
    {
        public string X { get; init; } = "";
        public string Y { get; init; } = "";
    }

    private void BtnBuild_OnClick(object sender, RoutedEventArgs e)
    {
        if (!double.TryParse(TxtFrom.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var xf)
            || !double.TryParse(TxtTo.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var xt)
            || !double.TryParse(TxtStep.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var step)
            || !double.TryParse(TxtB.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var b))
        {
            _lastPts = null;
            GridPoints.ItemsSource = null;
            PlotCanvas.Children.Clear();
            MessageBox.Show("Введите числа: x от, x до, шаг и b.", "График", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!Variant14Math.TrySamplePlot(xf, xt, step, b, out var pts, out var err))
        {
            _lastPts = null;
            GridPoints.ItemsSource = null;
            PlotCanvas.Children.Clear();
            MessageBox.Show(err ?? "Ошибка", "График", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        GridPoints.ItemsSource = pts.Select(p => new PointVm
        {
            X = p.X.ToString(CultureInfo.InvariantCulture),
            Y = p.Y.ToString(CultureInfo.InvariantCulture),
        }).ToList();

        DrawPolyline(pts);
    }

    private void DrawPolyline(IReadOnlyList<(double X, double Y)> pts)
    {
        _lastPts = pts;
        PlotCanvas.Children.Clear();
        TryRedraw();
    }

    private void TryRedraw()
    {
        var pts = _lastPts;
        if (pts == null || pts.Count == 0)
            return;

        PlotCanvas.UpdateLayout();
        double w = PlotCanvas.ActualWidth;
        double h = PlotCanvas.ActualHeight;
        if (w < 8 || h < 8)
        {
            PlotCanvas.SizeChanged -= PlotCanvas_OnSizeChanged;
            PlotCanvas.SizeChanged += PlotCanvas_OnSizeChanged;
            return;
        }

        PlotCanvas.SizeChanged -= PlotCanvas_OnSizeChanged;

        double minX = pts.Min(p => p.X);
        double maxX = pts.Max(p => p.X);
        double minY = pts.Min(p => p.Y);
        double maxY = pts.Max(p => p.Y);
        if (Math.Abs(maxX - minX) < 1e-15)
        {
            minX -= 1;
            maxX += 1;
        }

        if (Math.Abs(maxY - minY) < 1e-15)
        {
            minY -= 1;
            maxY += 1;
        }

        const double pad = 24;
        double sx = (w - 2 * pad) / (maxX - minX);
        double sy = (h - 2 * pad) / (maxY - minY);

        var screenPts = new List<System.Windows.Point>(pts.Count);
        foreach (var p in pts)
        {
            double px = pad + (p.X - minX) * sx;
            double py = h - pad - (p.Y - minY) * sy;
            screenPts.Add(new System.Windows.Point(px, py));
        }

        // Одна точка: ломаная не рисуется в WPF (нужно ≥2 вершин) — показываем маркер.
        if (screenPts.Count >= 2)
        {
            var pl = new Polyline
            {
                Stroke = Brushes.SteelBlue,
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round,
            };
            foreach (var pt in screenPts)
                pl.Points.Add(pt);
            PlotCanvas.Children.Add(pl);
        }

        const double marker = 7;
        foreach (var pt in screenPts)
        {
            var dot = new Ellipse
            {
                Width = marker,
                Height = marker,
                Fill = Brushes.SteelBlue,
                Stroke = Brushes.DarkSlateBlue,
                StrokeThickness = 1,
            };
            Canvas.SetLeft(dot, pt.X - marker / 2);
            Canvas.SetTop(dot, pt.Y - marker / 2);
            PlotCanvas.Children.Add(dot);
        }

        var axes = new System.Windows.Shapes.Path
        {
            Stroke = Brushes.Gray,
            StrokeThickness = 1,
        };
        var g = new StreamGeometry();
        using (var ctx = g.Open())
        {
            ctx.BeginFigure(new System.Windows.Point(pad, h - pad), false, false);
            ctx.LineTo(new System.Windows.Point(w - pad, h - pad), true, false);
            ctx.BeginFigure(new System.Windows.Point(pad, pad), false, false);
            ctx.LineTo(new System.Windows.Point(pad, h - pad), true, false);
        }

        g.Freeze();
        axes.Data = g;
        PlotCanvas.Children.Insert(0, axes);
    }

    private void PlotCanvas_OnSizeChanged(object sender, SizeChangedEventArgs e) => TryRedraw();
}
