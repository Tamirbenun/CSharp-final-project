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

namespace ToDo.Controls;

public partial class AddForm : UserControl
{
    public AddForm()
    {
        InitializeComponent();
    }

    private void Button_Add(object sender, RoutedEventArgs e)
    {
        if (TaskText.Text.Length == 0)
        {
            BorderTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            ErrorMessage.Visibility = Visibility.Visible;
        } else
        {
            var window = Window.GetWindow(this) as MainWindow;
            window.tasks.AddTask();
        }
    }

    private void Button_Exit(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
    }

    private void Button_Close(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
    }
}
