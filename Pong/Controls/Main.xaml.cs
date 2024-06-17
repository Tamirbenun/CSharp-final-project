using System;
using System.Collections.Generic;
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
using Pong.Controls;
using Pong;

namespace Pong.Controls;

public partial class Main : UserControl
{
    Options options = new Options();
    public Main()
    {
        InitializeComponent();
    }

    private void Button_NEWGAME(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.StartGame();

        this.Visibility = Visibility.Collapsed;
    }

    private void Button_OPTIONS(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.ContentControlNow.Content = options;
        this.Visibility = Visibility.Collapsed;
        options.Visibility = Visibility.Visible;
    }

    private void Button_EXIT(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.Close();
    }
}
