using Calculator.Controls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

public partial class MainWindow : Window
{
    CalculatorPage CalculatorPage = new CalculatorPage();
    MorePage MorePage = new MorePage();

    public MainWindow()
    {
        InitializeComponent();

        ContentControlPage.Content = CalculatorPage;
        
    }

    private void Button_Converter(object sender, RoutedEventArgs e)
    {
        var converter = new BrushConverter();
        var gray = (Brush)converter.ConvertFromString("#ABA6A3");
        var orange = (Brush)converter.ConvertFromString("#FF513C");
        MoreButton.Foreground = orange;
        CalculatorButton.Foreground = gray;
        ContentControlPage.Content = MorePage;
    }

    private void Button_Calculator(object sender, RoutedEventArgs e)
    {
        var converter = new BrushConverter();
        var gray = (Brush)converter.ConvertFromString("#ABA6A3");
        var orange = (Brush)converter.ConvertFromString("#FF513C");
        MoreButton.Foreground = gray;
        CalculatorButton.Foreground = orange;
        ContentControlPage.Content = CalculatorPage;
    }
}