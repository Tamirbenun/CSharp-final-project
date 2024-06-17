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

public partial class AddNoteForm : UserControl
{
    public AddNoteForm()
    {
        InitializeComponent();
    }

    private void Button_Add(object sender, RoutedEventArgs e)
    {
        if (NoteTag.Text == "" || NoteHeader.Text == "" || NoteText.Text == "")
        {
            if (NoteTag.Text == "")
            {
                NoteTag.BorderBrush = new SolidColorBrush(Colors.Red);
                NoteTag.BorderThickness = new Thickness(1);
            }

            if (NoteHeader.Text == "")
            {
                NoteHeader.BorderBrush = new SolidColorBrush(Colors.Red);
                NoteHeader.BorderThickness = new Thickness(1);
            }

            if (NoteText.Text == "")
            {
                NoteText.BorderBrush = new SolidColorBrush(Colors.Red);
                NoteText.BorderThickness = new Thickness(1);
            }
        } else
        {
            var window = Window.GetWindow(this) as MainWindow;
            if (window.notes.isEdit == false)
            {
                window.notes.AddNote();
            } else
            {
                window.notes.EditNote();
            }
        }
    }

    private void Button_Close(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
    }

    private void Button_Exit(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
    }
}
