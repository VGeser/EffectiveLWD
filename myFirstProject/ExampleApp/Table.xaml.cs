using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SimulatorSubsystem;

namespace ExampleApp
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table
    {
        private readonly TableDataHolder _holder;

        public Table()
        {
            InitializeComponent();
            _holder = new TableDataHolder();
            FilePrinting();
        }

        private void FilePrinting()
        {
            string file = Download.f_name;
            int slash = file.LastIndexOf("\\");
            FileName.Text = file.Substring(slash + 1);
        //     //FileName.Text = "Hello";
        }
        public Boolean Rotor_val()
        {
            return Rotor.IsChecked != null && Rotor.IsChecked.Value;
        }

        public Boolean TFG_val()
        {
            return Rotor.IsChecked != null && Rotor.IsChecked.Value;
        }

        public Boolean Stat_val()
        {
            return Rotor.IsChecked != null && Rotor.IsChecked.Value;
        }

        private void Rotor_Checked(object sender, RoutedEventArgs e)
        {
            Rotor.IsChecked = true;
        }

        private void Rotor_Unchecked(object sender, RoutedEventArgs e)
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
            bool isNumber = Int32.TryParse(textBox.Text, out _holder.Frequency);
            //MessageBox.Show( "isNumber " + isNumber);
            if (!isNumber && textBox.Text.Length > 0)
            {
                var error = "Frequency может принимать только неотрицательные значения!";
                MessageBox.Show(error);
            }
        }

        private void TextChanged_InitialPasses(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool isNumber = Int32.TryParse(textBox.Text, out _holder.InitialPasses);
            if (!isNumber && textBox.Text.Length > 0)
            {
                var error = "Initial Passes может принимать только неотрицательные значения!";
                MessageBox.Show(error);
            }
        }

        private void TextChanged_Symbols(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            TextBox textBox = (TextBox)sender;
            bool isNumber = Int32.TryParse(textBox.Text, out _holder.Symbols);
            if (!isNumber && textBox.Text.Length > 0)
            {
                var error = "Symbols может принимать только неотрицательные значения!";
                MessageBox.Show(error);
            }
        }

        private void TextChanged_Name(object sender, TextChangedEventArgs e)
        {
            // NameText.Select(0, 0);
            TextBox textBox = (TextBox)sender;
            Name = textBox.Text;
            // NameText.Select(NameText.Text.Length, 0);
            _holder.Name = textBox.Text;
        }

        private void TextChanged_x1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Int32.TryParse(textBox.Text, out _holder.X1);
            if (textBox.Text.Length == 0)
            {
                var error = "Поле не может оставаться пустым!";
                MessageBox.Show(error);
            }
        }

        private void TextChanged_x2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool isNumber = Int32.TryParse(textBox.Text, out _holder.X2);
            string error;
            if (!isNumber && textBox.Text.Length > 0)
            {
                error = "Некорректное значение!";
                MessageBox.Show(error);
            }

            if (_holder.X2 < _holder.X1)
            {
                error = "Значение не может быть меньше предыдущего!";
                MessageBox.Show(error);
            }
        }

        private void TextChanged_Center(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool isNumber = Int32.TryParse(textBox.Text, out _holder.Center);
            string error = String.Empty;
            if (!isNumber && textBox.Text.Length > 0)
            {
                error = "Некорректное значение!";
                MessageBox.Show(error);
            }

            if ((_holder.Center < _holder.X1) || (_holder.Center >= _holder.X2) && error.Length == 0)
            {
                error = "Значение не может выходить за пределы диапазона!";
                MessageBox.Show(error);
            }
        }

        private void TextChanged_Step(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool isNumber = Int32.TryParse(textBox.Text, out _holder.Step);
            string error = String.Empty;
            if (!isNumber && textBox.Text.Length > 0)
            {
                error = "Некорректное значение!";
                MessageBox.Show(error);
            }

            if (_holder.Step <= 0 && error.Length == 0 && textBox.Text.Length > 0)
            {
                error = "Значение должно быть только положительным!";
                MessageBox.Show(error);
            }

            if (_holder.Step >= (_holder.X2 - _holder.X1) && error.Length == 0 && textBox.Text.Length > 0)
            {
                error = "Значение не может быть меньше диапазона!";
                MessageBox.Show(error);
            }
        }

        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(_holder.ToString());
        //}

        private void Button_Simulation(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Result());
        }
    }
}