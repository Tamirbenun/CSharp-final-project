using System.Windows;
using System.Windows.Controls;

namespace ProjectGallery.Controls
{
    /// <summary>
    /// Interaction logic for InfoToDo.xaml
    /// </summary>
    public partial class InfoToDo : UserControl
    {
        ToDo.MainWindow ToDoProject;
        public InfoToDo()
        {
            InitializeComponent();
        }

        private void Button_RunToDo(object sender, RoutedEventArgs e)
        {
            ToDoProject = new ToDo.MainWindow();
            ToDoProject.Show();
        }

        private void Button_InfoTodoClose(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            var window = Window.GetWindow(this) as MainWindow;
            window.Column.Width = new System.Windows.GridLength(0);
        }

        
    }
}
