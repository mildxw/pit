using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PitPr4.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        RootFrame.Navigate(new GPage());
    }

    private void MenuG_OnClick(object sender, RoutedEventArgs e) => RootFrame.Navigate(new GPage());

    private void MenuD_OnClick(object sender, RoutedEventArgs e) => RootFrame.Navigate(new DPage());

    private void MenuPlot_OnClick(object sender, RoutedEventArgs e) => RootFrame.Navigate(new PlotPage());

    private void MenuExit_OnClick(object sender, RoutedEventArgs e) => Close();

    protected override void OnClosing(CancelEventArgs e)
    {
        var r = MessageBox.Show(
            "Закрыть приложение?",
            "Выход",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (r != MessageBoxResult.Yes)
            e.Cancel = true;
        base.OnClosing(e);
    }
}
