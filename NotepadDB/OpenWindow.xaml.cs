using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace NotepadDB
{
    /// <summary>
    /// Interaction logic for OpenWindow.xaml
    /// </summary>
    public partial class OpenWindow : Window
    {
        ApplicationContext db;

        public OpenWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Files.LoadAsync();
            this.DataContext = db.Files.Local.ToBindingList();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (filesList.SelectedItem == null) return;
            File file = filesList.SelectedItem as File;
            MainWindow main = new MainWindow(file);
            this.DialogResult = true;

            if (main.ShowDialog() == true)
            {
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (filesList.SelectedItem == null) return;
            File fileDelete = filesList.SelectedItem as File;

            db.Files.Remove(fileDelete);
            db.SaveChangesAsync();

            MessageBox.Show("File succesful delete", "Succesful!");
        }
    }
}
