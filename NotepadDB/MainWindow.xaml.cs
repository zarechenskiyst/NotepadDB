using Microsoft.Win32;
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

namespace NotepadDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            this.file = new File();
            InitializeWindow();
        }
        public File file { get; private set; }
        public MainWindow(File fileDB)
        {
            this.file = fileDB;
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            InitializeComponent();
            this.file = file;
            DataContext = file;
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow save = new SaveWindow(file);
            if (string.IsNullOrEmpty(file.Contents))
            {
                MessageBox.Show("File is missing", "Error");
                return;
            }
            if (save.ShowDialog() == true)
            {
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow open = new OpenWindow();
            if (open.ShowDialog() == true)
            {
            }
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            double x;
            double.TryParse(cmbFontSize.Text, out x);
            MainTextBox.FontSize = x;
        }
    }
}
