using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class Note : INotifyPropertyChanged
        {
            private string title;
            public string Title
            {
                get { return title; }
                set
                {
                    if (title != value)
                    {
                        title = value;
                        OnPropertyChanged();
                    }
                }
            }

            private string content;
            public string Content
            {
                get { return content; }
                set
                {
                    if (content != value)
                    {
                        content = value;
                        OnPropertyChanged();
                    }
                }
            }

            private DateTime date;
            public DateTime Date
            {
                get { return date; }
                set
                {
                    if (date != value)
                    {
                        date = value;
                        OnPropertyChanged();
                    }
                }
            }

            public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            // Implement commands for adding, editing, and deleting notes
        }

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected note
            Note selectedNote = ListView.SelectedItem as Note;

            // Update the read-only blocks with the selected note's information
            if (selectedNote != null)
            {
                TitleTextBox.Text = selectedNote.Title;
                NoteTextBox.Text = selectedNote.Content; // Assuming Content is a property of the Note class
            }
            else
            {
                TitleTextBox.Text = string.Empty;
                NoteTextBox.Text = string.Empty;
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            // If search text is empty, show all notes
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ListView.ItemsSource = Notes;
            }
            else
            {
                // Filter notes based on the search text
                var filteredNotes = Notes.Where(note => note.Title.ToLower().Contains(searchText) ||
                                                        note.Content.ToLower().Contains(searchText));
                ListView.ItemsSource = filteredNotes;
            }
        }

        public void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note
            {
                Title = AddTitleTextBox.Text,
                Content = AddNoteTextBox.Text,
                Date = DateTime.Now
            };

            Notes.Add(newNote);

            AddTitleTextBox.Clear();
            AddNoteTextBox.Clear();
        }
    }
}