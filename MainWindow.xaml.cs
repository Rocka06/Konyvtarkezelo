using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
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
using System.Xml.Serialization;

namespace Dolgozat
{
    public partial class MainWindow : Window
    {
        string[] genres = ["Regény", "Tudományos", "Fikció"];
        List<Book> books = new();
        
        public MainWindow()
        {
            InitializeComponent();
            
            foreach(string genre in genres)
            {
                genreComboBox.Items.Add(genre);
            }
            
            filterBooksComboBox.Items.Add("Összes");
            filterBooksComboBox.SelectedIndex = 0;
            foreach (string genre in genres)
            {
                filterBooksComboBox.Items.Add(genre);
            }
        }

        //events
        private void OnAddBookClick(object sender, RoutedEventArgs e)
        {
            string title = titleTextBox.Text;
            int genre = genreComboBox.SelectedIndex;
            
            if(title.Trim() == "")
            {
                MessageBox.Show("Üres cím!", "Hiba");
                return;
            }

            if(genre >= genres.Length || genre < 0)
            {
                MessageBox.Show("Érvénytelen műfaj!", "Hiba");
                return;
            }

            books.Add(new(title, genres[genre]));
            OnFilterChanged(null, null);
            ClearInputs();
        }
        private void OnShowBooksClick(object sender, RoutedEventArgs e)
        {
            if (books.Count > 0)
            {
                string text = "Összes könyv:\n";
                foreach (Book book in books)
                {
                    text += book.ToString() + "\n";
                }
                MessageBox.Show(text, "Összes könyv");
            }
            else
            {
                MessageBox.Show("Nincs megjelenítendő könyv.");
            }
        }
        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs kiválasztott elem", "Hiba");
                return;
            }

            books.Remove(booksListBox.SelectedItem as Book);
            OnFilterChanged(null, null);

            deleteButton.IsEnabled = false;
            editButton.IsEnabled = false;
        }
        private void OnEditClicked(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs kiválasztott elem", "Hiba");
                return;
            }

            EditWindow editWindow = new();
            editWindow.saveButton.Click += (a,b) => SaveEditClicked(editWindow);

            //Assigning the values of the window
            Book selectedBook = booksListBox.SelectedItem as Book;

            foreach (string genre in genres)
            {
                editWindow.genreComboBox.Items.Add(genre);
            }
            editWindow.genreComboBox.SelectedItem = selectedBook.Genre;
            editWindow.titleTextBox.Text = selectedBook.Title;


            editWindow.ShowDialog();
        }

        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterBooksComboBox.SelectedIndex == 0) RenderList();
            else RenderList(genres[filterBooksComboBox.SelectedIndex - 1]);
        }
        private void OnBooksListsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isEnabled = booksListBox.SelectedIndex == -1;

            deleteButton.IsEnabled = IsEnabled;
            editButton.IsEnabled = IsEnabled;
        }
        
        private void SaveEditClicked(EditWindow editWindow)
        {
            Book newBook = new(editWindow.titleTextBox.Text, genres[editWindow.genreComboBox.SelectedIndex]);
            books[books.IndexOf(booksListBox.SelectedItem as Book)] = newBook;
            OnFilterChanged(null, null);
            editWindow.Close();
            deleteButton.IsEnabled = false;
            editButton.IsEnabled = false;
        }


        private void RenderList()
        {
            booksListBox.Items.Clear();
            foreach(Book book in books)
            {
                booksListBox.Items.Add(book);
            }
        }
        private void RenderList(string genre)
        {
            booksListBox.Items.Clear();
            foreach (Book book in books)
            {
                if (book.Genre != genre) continue;
                booksListBox.Items.Add(book);
            }
        }
        private void ClearInputs()
        {
            titleTextBox.Text = "";
            genreComboBox.SelectedIndex = -1;
        }

        
    }
}