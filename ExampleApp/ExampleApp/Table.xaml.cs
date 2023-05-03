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

namespace ExampleApp
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : Page
    {
        public static int Frequency;
        public static int InitialPasses;
        public static int Symbols;
        public static string Name;
        public static int x1;
        public static int x2;
        public static int Step;
        public static int Center;
        public Table()
        {
            InitializeComponent();
            //FilePrinting();
        }

        private void FilePrinting()
        {
            string file = Download.f_name;
            int slash = file.LastIndexOf("\\");
            FileName.Text = file.Substring(slash + 1);
            //  FileName.Text = "Hello";
        }

        public void Rotor_Checked(object sender, RoutedEventArgs e)
            {
                Rotor.IsChecked = true;

            }
            public void Rotor_Unchecked(object sender, RoutedEventArgs e)
            {
                Rotor.IsChecked = false;
            }
            private void Stat_Checked(object sender, RoutedEventArgs e)
            {
                Stat.IsChecked = true;

            }
            private void Stat_Unchecked(object sender, RoutedEventArgs e)
            {
                Stat.IsChecked = false;
            }

            private void tfgFlag_Checked(object sender, RoutedEventArgs e)
            {
                tfgFlag.IsChecked = true;

            }
            private void tfgFlag_Unchecked(object sender, RoutedEventArgs e)
            {
                tfgFlag.IsChecked = false;
            }
            private void TextChanged_Frequency(object sender, TextChangedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out Frequency);
                //MessageBox.Show( "isNumber " + isNumber);
                string error = String.Empty;
                if (!isNumber && textBox.Text.Length > 0)
                {
                    error = "Frequency может принимать только неотрицательные значения!";
                    MessageBox.Show(error);
                }
            }
            private void TextChanged_InitialPasses(object sender, TextChangedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out InitialPasses);
                string error = String.Empty;
                if (!isNumber && textBox.Text.Length > 0)
                {
                    error = "Initial Passes может принимать только неотрицательные значения!";
                    MessageBox.Show(error);
                }
            }

            private void TextChanged_Symbols(object sender, TextChangedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out Symbols);
                string error = String.Empty;
                if (!isNumber && textBox.Text.Length > 0)
                {
                    error = "Symbols может принимать только неотрицательные значения!";
                    MessageBox.Show(error);
                }
            }

            private void TextChanged_Name(object sender, TextChangedEventArgs e)
            {
                NameText.Select(0, 0);
                TextBox textBox = (TextBox)sender;
                Name = textBox.Text;
                NameText.Select(NameText.Text.Length, 0);
            }

            private void TextChanged_x1(object sender, TextChangedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out x1);
                string error = String.Empty;
                if (textBox.Text.Length == 0)
                {
                    error = "Поле не может оставаться пустым!";
                    MessageBox.Show(error);
                }
            }
            private void TextChanged_x2(object sender, RoutedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out x2);
                string error = String.Empty;
                if (!isNumber && textBox.Text.Length > 0)
                {
                    error = "Некорректное значение!";
                    MessageBox.Show(error);
                }
                if (x2 < x1)
                {
                    error = "Значение не может быть меньше предыдущего!";
                    MessageBox.Show(error);
                }
            }

            private void TextChanged_Center(object sender, RoutedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out Center);
                string error = String.Empty;
                if (!isNumber && textBox.Text.Length > 0)
                {
                    error = "Некорректное значение!";
                    MessageBox.Show(error);
                }
                if ((Center < x1) || (Center >= x2) && error.Length == 0)
                {
                    error = "Значение не может выходить за пределы диапазона!";
                    MessageBox.Show(error);
                }
            }

            private void TextChanged_Step(object sender, RoutedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                bool isNumber = Int32.TryParse(textBox.Text, out Step);
                string error = String.Empty;
                if (!isNumber && textBox.Text.Length > 0)
                {
                    error = "Некорректное значение!";
                    MessageBox.Show(error);
                }
                if (Step <= 0 && error.Length == 0 && textBox.Text.Length > 0)
                {
                    error = "Значение должно быть только положительным!";
                    MessageBox.Show(error);
                }
                if (Step < (x2 - x1) && error.Length == 0 && textBox.Text.Length > 0)
                {
                    error = "Значение не может быть меньше диапазона!";
                    MessageBox.Show(error);
                }
            }

       


    }

}

