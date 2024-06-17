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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace ToDo.Controls;

public partial class Notes : UserControl
{
    public AddNoteForm addNoteForm = new AddNoteForm();
    public DeleteWindow deleteWindow = new DeleteWindow();
    public string notesData;
    public bool isEdit = false;

    public Notes()
    {
        InitializeComponent();
        DisplayNotes();
    }

    public void DisplayNotes()
    {
        try
        {
            notesData = File.ReadAllText("Notes.json");
            var notes = JsonSerializer.Deserialize<List<NotesJson>>(notesData);

            ListOfNotes.ItemsSource = notes;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void AddNote()
    {
        try
        {
            notesData = File.ReadAllText("Notes.json");
            var notes = JsonSerializer.Deserialize<List<NotesJson>>(notesData);

            var newNote = new NotesJson
            {
                Create = DateTime.Now,
                Update = DateTime.Now,
                Tag = addNoteForm.NoteTag.Text,
                Header = addNoteForm.NoteHeader.Text,
                Text = addNoteForm.NoteText.Text
            };

            notes.Insert(0, newNote);

            string note = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Notes.json", note);

            addNoteForm.Visibility = Visibility.Collapsed;

            DisplayNotes();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    public void EditNote()
    {
        try
        {
            notesData = File.ReadAllText("Notes.json");
            var notes = JsonSerializer.Deserialize<List<NotesJson>>(notesData);

            var newNote = new NotesJson
            {
                Create = DateTime.Now,
                Update = DateTime.Now,
                Tag = addNoteForm.NoteTag.Text,
                Header = addNoteForm.NoteHeader.Text,
                Text = addNoteForm.NoteText.Text
            };

            int selectedIndex = ListOfNotes.SelectedIndex;
            notes.RemoveAt(selectedIndex);

            notes.Insert(0, newNote);

            string note = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Notes.json", note);

            addNoteForm.Visibility = Visibility.Collapsed;

            DisplayNotes();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    private void Button_AddNote(object sender, RoutedEventArgs e)
    {
        isEdit = false;
        var window = Window.GetWindow(this) as MainWindow;
        window.ControlNewWindow.Content = addNoteForm;
        addNoteForm.Visibility = Visibility.Visible;
    }

    private void Button_EditNote(object sender, RoutedEventArgs e)
    {
        isEdit = true;
        notesData = File.ReadAllText("Notes.json");
        var notes = JsonSerializer.Deserialize<List<NotesJson>>(notesData);
        int selectedIndex = ListOfNotes.SelectedIndex;

        if (selectedIndex != -1)
        {
            var window = Window.GetWindow(this) as MainWindow;
            window.ControlNewWindow.Content = addNoteForm;

            addNoteForm.HeaderFormText.Text = "Edit Note";
            addNoteForm.ButtonAdd.Width = 90;
            addNoteForm.AddButtonContent.Text = "Confirm";

            addNoteForm.NoteTag.Text = notes[selectedIndex].Tag.ToString();
            addNoteForm.NoteHeader.Text = notes[selectedIndex].Header.ToString();
            addNoteForm.NoteText.Text = notes[selectedIndex].Text.ToString();

            addNoteForm.Visibility = Visibility.Visible;
        }
    }

    public void DeleteNote()
    {
        int selectedIndex = ListOfNotes.SelectedIndex;

        if (selectedIndex != -1)
        {
            notesData = File.ReadAllText("Notes.json");
            var notes = JsonSerializer.Deserialize<List<NotesJson>>(notesData);

            notes.RemoveAt(selectedIndex);

            string newNotesList = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Notes.json", newNotesList);

            deleteWindow.Visibility = Visibility.Collapsed;

            DisplayNotes();
        }
    }

    private void Button_Deleted(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        deleteWindow.WhoCallMe = "Notes";
        window.ControlNewWindow.Content = deleteWindow;
        deleteWindow.Visibility = Visibility.Visible;
    }
}

public class NotesJson
{
    public DateTime Create { get; set; }
    public DateTime Update { get; set; }
    public string Tag { get; set; }
    public string Header { get; set; }
    public string Text { get; set; }
}