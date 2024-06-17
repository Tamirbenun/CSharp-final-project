using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
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

public partial class Tasks : UserControl
{
    DeleteWindow deleteWindow = new DeleteWindow();
    AddForm addForm = new AddForm();
    public string tasksData;
    public string completedData;

    public Tasks()
    {
        InitializeComponent();
        DisplayTasks();
    }

    public void DisplayTasks()
    {
        try
        {
            tasksData = File.ReadAllText("Tasks.json");
            var tasks = JsonSerializer.Deserialize<List<TasksJson>>(tasksData);

            ListOfTasks.ItemsSource = tasks;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void AddTask()
    {
        try
        {
            tasksData = File.ReadAllText("Tasks.json");
            var tasks = JsonSerializer.Deserialize<List<TasksJson>>(tasksData);

            var newTask = new TasksJson
            {
                Create = DateTime.Now,
                Update = DateTime.Now,
                Text = addForm.TaskText.Text,
            };

            tasks.Insert(0, newTask);

            string task = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Tasks.json", task);

            addForm.Visibility = Visibility.Collapsed;

            DisplayTasks();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    private void Button_ADD(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.ControlNewWindow.Content = addForm;
        addForm.Visibility = Visibility.Visible;
    }

    private void Button_Completed(object sender, RoutedEventArgs e)
    {
        try
        {
            tasksData = File.ReadAllText("Tasks.json");
            var tasks = JsonSerializer.Deserialize<List<TasksJson>>(tasksData);

            completedData = File.ReadAllText("Completed.json");
            var tasksCompleted = JsonSerializer.Deserialize<List<TasksJson>>(completedData);

            int selectedIndex = ListOfTasks.SelectedIndex;

            var selectedTask = new TasksJson
            {
                Create = tasks[selectedIndex].Create,
                Update = DateTime.Now,
                Text = tasks[selectedIndex].Text,
            };

            tasks.RemoveAt(selectedIndex);
            tasksCompleted.Insert(0, selectedTask);

            string newTasksList = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Tasks.json", newTasksList);

            string newCompletedList = JsonSerializer.Serialize(tasksCompleted, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Completed.json", newCompletedList);

            DisplayTasks();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void DeleteTask()
    {
        try
        {
            int selectedIndex = ListOfTasks.SelectedIndex;

            tasksData = File.ReadAllText("Tasks.json");
            var tasks = JsonSerializer.Deserialize<List<TasksJson>>(tasksData);

            tasks.RemoveAt(selectedIndex);

            string newTasksList = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Tasks.json", newTasksList);

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
        deleteWindow.WhoCallMe = "Tasks";
        window.ControlNewWindow.Content = deleteWindow;
        deleteWindow.Visibility = Visibility.Visible;
    }
}

public class TasksJson
{
    public DateTime Create { get; set; }
    public DateTime Update { get; set; }
    public string Text { get; set; }
}
