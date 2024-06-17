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
public partial class DeleteWindow : UserControl
{
    public string WhoCallMe;
    public DeleteWindow()
    {
        InitializeComponent();
    }

    private void Button_Delete(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;

        if (WhoCallMe == "Notes")
        {
            window.notes.DeleteNote();
        }else if (WhoCallMe == "Tasks")
        {
            window.tasks.DeleteTask();
        } else if (WhoCallMe == "Completed")
        {
            window.tasksCompleted.DeleteCompleted();
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
