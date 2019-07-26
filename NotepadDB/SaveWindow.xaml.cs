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
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        File file { get; set; }
        public SaveWindow(File file)
        {
            InitializeComponent();
            this.file = file;
            SaveBox.Text = file.Name;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SaveBox.Text))
            {
                MessageBox.Show("File name is missing", "Error");
                return;
            }
            file.Name = SaveBox.Text;
            using (ApplicationContext db = new ApplicationContext())
            {
                if (CheckNameInDB(db))
                {
                    db.Files.Add(file);
                    db.SaveChangesAsync();
                    this.DialogResult = true;
                    MessageBox.Show("File successful saved", "Successful!");
                }
                else return;
            }
        }

        private bool CheckNameInDB(ApplicationContext context)
        {
            context.Files.LoadAsync();
            var files = context.Files.Local.ToBindingList();
            foreach (var item in files)
                if (item.Name == file.Name)
                {
                    MessageBox.Show("File with this name already exist", "Error");
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to rewrite file?", "Rewrite", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        context.Files.Remove(item);
                        context.SaveChanges();
                        return true;
                    }
                        return false;
                }
            return true;
        }
    }
}
