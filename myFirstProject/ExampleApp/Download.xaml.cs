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
using System.IO;
using System.IO.Enumeration;

namespace ExampleApp
{
    /// <summary>
    /// Логика взаимодействия для Download.xaml
    /// </summary>
    public partial class Download : Page
    {
        internal static string f_name;

        public Download()
        {
            InitializeComponent();
        }

        private void HandleClick(object sender, RoutedEventArgs e)
        {
            OpenFile_Click(sender, e);
            Transition(sender, e);

        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "LAS files (*.las)|*.las";
            if (openFileDialog.ShowDialog() == true)
            {
               f_name = openFileDialog.FileName;
               // txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
              //txtEditor.Text = openFileDialog.FileName;
            }

        }
        private void Transition(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Table());

        }
    }
}
