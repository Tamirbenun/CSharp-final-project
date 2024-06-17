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

namespace ProjectGallery.Controls;
public partial class InfoPong : UserControl
{
    Pong.MainWindow PongProject;

    public InfoPong()
    {
        InitializeComponent();
    }

    private void Button_RunPong(object sender, RoutedEventArgs e)
    {
        PongProject = new Pong.MainWindow();
        PongProject.Show();
    }

    private void Button_InfoPongClose(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
        var window = Window.GetWindow(this) as MainWindow;
        window.Column.Width = new System.Windows.GridLength(0);
    }
}
