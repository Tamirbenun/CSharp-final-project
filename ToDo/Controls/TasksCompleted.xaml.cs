using System;
using System.Collections.Generic;
using System.IO;
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
using System.Text.Json;

namespace ToDo.Controls;

public partial class TasksCompleted : UserControl
{
    DeleteWindow deleteWindow = new DeleteWindow();
    public string completedData;
    public string tasksData;
    public TasksCompleted()
    {
        InitializeComponent();
        DisplayTasks();
    }

    public void DisplayTasks()
    {
        try
        {
            completedData = File.ReadAllText("Completed.json");
            var tasksCompleted = JsonSerializer.Deserialize<List<TasksJson>>(completedData);

            ListOfTasks.ItemsSource = tasksCompleted;
        } 
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    private void Button_Restore(object sender, RoutedEventArgs e)
    {
        try
        {
            completedData = File.ReadAllText("Completed.json");
            var tasksCompleted = JsonSerializer.Deserialize<List<TasksJson>>(completedData);

            tasksData = File.ReadAllText("Tasks.json");
            var tasks = JsonSerializer.Deserialize<List<TasksJson>>(tasksData);

            int selectedIndex = ListOfTasks.SelectedIndex;

            var selectedTask = new TasksJson
            {
                Create = tasksCompleted[selectedIndex].Create,
                Update = DateTime.Now,
                Text = tasksCompleted[selectedIndex].Text,
            };

            tasksCompleted.RemoveAt(selectedIndex);
            tasks.Insert(0, selectedTask);

            string newCompletedList = JsonSerializer.Serialize(tasksCompleted, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Completed.json", newCompletedList);

            string newTasksList = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Tasks.json", newTasksList);

            DisplayTasks();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void DeleteCompleted()
    {
        try
        {
            int selectedIndex = ListOfTasks.SelectedIndex;

            completedData = File.ReadAllText("Completed.json");
            var tasksCompleted = JsonSerializer.Deserialize<List<TasksJson>>(completedData);

            tasksCompleted.RemoveAt(selectedIndex);

            string newCompletedList = JsonSerializer.Serialize(tasksCompleted, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Completed.json", newCompletedList);

            deleteWindow.Visibility = Visibility.Collapsed;

            DisplayTasks();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    private void Button_Deleted(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        deleteWindow.WhoCallMe = "Completed";
        window.ControlNewWindow.Content = deleteWindow;
        deleteWindow.Visibility = Visibility.Visible;
    }
}
