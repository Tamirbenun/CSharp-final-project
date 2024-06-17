using System.Diagnostics;
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
using ToDo.Controls;

namespace ToDo;
public partial class MainWindow : Window
{
    public ToDo.Controls.Calendar calendar = new ToDo.Controls.Calendar();
    public Tasks tasks = new Tasks();
    public Notes notes = new Notes();
    public TasksCompleted tasksCompleted = new TasksCompleted();

    BitmapImage TasksIconMarked = new BitmapImage(new Uri("Resources/TasksMarked.png", UriKind.RelativeOrAbsolute));

    public MainWindow()
    {
        InitializeComponent();
        Control.Content = tasks;
        var TasksColorMarked = (Brush)new BrushConverter().ConvertFromString("#0086FF");
        Btn_Tasks_Text.Foreground = TasksColorMarked;
        Btn_Tasks_Icon.Source = TasksIconMarked;
    }

    private void Button_Notes(object sender, RoutedEventArgs e)
    {
        Control.Content = notes;
        ButtonsMarked("Notes");
        notes.DisplayNotes();
    }

    private void Button_Calendar(object sender, RoutedEventArgs e)
    {
        Control.Content = calendar;
        ButtonsMarked("Calendar");
    }

    private void Button_Tasks(object sender, RoutedEventArgs e)
    {
        Control.Content = tasks;
        ButtonsMarked("Tasks");
        tasks.DisplayTasks();
    }

    private void Button_Completed(object sender, RoutedEventArgs e)
    {
        Control.Content = tasksCompleted;
        ButtonsMarked("Completed");
        tasksCompleted.DisplayTasks();
    }

    private void ButtonsMarked(string btn)
    {
        if (btn == "Notes")
        {
            var Marked = (Brush)new BrushConverter().ConvertFromString("#0086FF");
            BitmapImage NotesMarked = new BitmapImage(new Uri("Resources/NotesMarked.png", UriKind.RelativeOrAbsolute));
            Btn_Notes_Text.Foreground = Marked;
            Btn_Notes_Icon.Source = NotesMarked;

            var NotMarked = (Brush)new BrushConverter().ConvertFromString("#666666");
            BitmapImage CalendarNotMarked = new BitmapImage(new Uri("Resources/Calendar.png", UriKind.RelativeOrAbsolute));
            BitmapImage TasksNotMarked = new BitmapImage(new Uri("Resources/Tasks.png", UriKind.RelativeOrAbsolute));
            BitmapImage CompletedNotMarked = new BitmapImage(new Uri("Resources/Completed.png", UriKind.RelativeOrAbsolute));
            BitmapImage TrashNotMarked = new BitmapImage(new Uri("Resources/Trash.png", UriKind.RelativeOrAbsolute));
            Btn_Calendar_Text.Foreground = NotMarked;
            Btn_Calendar_Icon.Source = CalendarNotMarked;
            Btn_Tasks_Text.Foreground = NotMarked;
            Btn_Tasks_Icon.Source = TasksNotMarked;
            Btn_Completed_Text.Foreground = NotMarked;
            Btn_Completed_Icon.Source = CompletedNotMarked;
        } else if (btn == "Calendar")
        {
            var Marked = (Brush)new BrushConverter().ConvertFromString("#0086FF");
            BitmapImage CalendarMarked = new BitmapImage(new Uri("Resources/CalendarMarked.png", UriKind.RelativeOrAbsolute));
            Btn_Calendar_Text.Foreground = Marked;
            Btn_Calendar_Icon.Source = CalendarMarked;

            var NotMarked = (Brush)new BrushConverter().ConvertFromString("#666666");
            BitmapImage NotesNotMarked = new BitmapImage(new Uri("Resources/Notes.png", UriKind.RelativeOrAbsolute));
            BitmapImage TasksNotMarked = new BitmapImage(new Uri("Resources/Tasks.png", UriKind.RelativeOrAbsolute));
            BitmapImage CompletedNotMarked = new BitmapImage(new Uri("Resources/Completed.png", UriKind.RelativeOrAbsolute));
            BitmapImage TrashNotMarked = new BitmapImage(new Uri("Resources/Trash.png", UriKind.RelativeOrAbsolute));
            Btn_Notes_Text.Foreground = NotMarked;
            Btn_Notes_Icon.Source = NotesNotMarked;
            Btn_Tasks_Text.Foreground = NotMarked;
            Btn_Tasks_Icon.Source = TasksNotMarked;
            Btn_Completed_Text.Foreground = NotMarked;
            Btn_Completed_Icon.Source = CompletedNotMarked;
        } else if (btn == "Tasks")
        {
            var TasksColorMarked = (Brush)new BrushConverter().ConvertFromString("#0086FF");
            Btn_Tasks_Text.Foreground = TasksColorMarked;
            Btn_Tasks_Icon.Source = TasksIconMarked;

            var NotMarked = (Brush)new BrushConverter().ConvertFromString("#666666");
            BitmapImage NotesNotMarked = new BitmapImage(new Uri("Resources/Notes.png", UriKind.RelativeOrAbsolute));
            BitmapImage CalendarNotMarked = new BitmapImage(new Uri("Resources/Calendar.png", UriKind.RelativeOrAbsolute));
            BitmapImage CompletedNotMarked = new BitmapImage(new Uri("Resources/Completed.png", UriKind.RelativeOrAbsolute));
            BitmapImage TrashNotMarked = new BitmapImage(new Uri("Resources/Trash.png", UriKind.RelativeOrAbsolute));
            Btn_Notes_Text.Foreground = NotMarked;
            Btn_Notes_Icon.Source = NotesNotMarked;
            Btn_Calendar_Text.Foreground = NotMarked;
            Btn_Calendar_Icon.Source = CalendarNotMarked;
            Btn_Completed_Text.Foreground = NotMarked;
            Btn_Completed_Icon.Source = CompletedNotMarked;
        } else if (btn == "Completed")
        {
            var Marked = (Brush)new BrushConverter().ConvertFromString("#0086FF");
            BitmapImage CompletedMarked = new BitmapImage(new Uri("Resources/CompletedMarked.png", UriKind.RelativeOrAbsolute));
            Btn_Completed_Text.Foreground = Marked;
            Btn_Completed_Icon.Source = CompletedMarked;

            var NotMarked = (Brush)new BrushConverter().ConvertFromString("#666666");
            BitmapImage NotesNotMarked = new BitmapImage(new Uri("Resources/Notes.png", UriKind.RelativeOrAbsolute));
            BitmapImage CalendarNotMarked = new BitmapImage(new Uri("Resources/Calendar.png", UriKind.RelativeOrAbsolute));
            BitmapImage TasksNotMarked = new BitmapImage(new Uri("Resources/Tasks.png", UriKind.RelativeOrAbsolute));
            BitmapImage TrashNotMarked = new BitmapImage(new Uri("Resources/Trash.png", UriKind.RelativeOrAbsolute));
            Btn_Notes_Text.Foreground = NotMarked;
            Btn_Notes_Icon.Source = NotesNotMarked;
            Btn_Tasks_Text.Foreground = NotMarked;
            Btn_Tasks_Icon.Source = TasksNotMarked;
            Btn_Calendar_Text.Foreground = NotMarked;
            Btn_Calendar_Icon.Source = CalendarNotMarked;
        }
    }
}