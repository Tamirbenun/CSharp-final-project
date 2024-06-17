using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectGallery.Controls;
public partial class InfoCalculator : UserControl
{
    Calculator.MainWindow CalculatorProject;
    public InfoCalculator()
    {
        InitializeComponent();
    }

    private void Button_RunCalculator(object sender, RoutedEventArgs e)
    {
        CalculatorProject = new Calculator.MainWindow();
        CalculatorProject.Show();
    }

    private void Button_InfoCalculatorClose(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
        var window = Window.GetWindow(this) as MainWindow;
        window.Column.Width = new System.Windows.GridLength(0);
    }
}
