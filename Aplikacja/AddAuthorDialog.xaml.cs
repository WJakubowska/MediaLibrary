using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Aplikacja
{

    public class AuthorNameValidation : ValidationRule
    {
        private MainWindow mainWindow;

        public AuthorNameValidation(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string) value))
            {
                return new ValidationResult(false, "Author name can not be empty");
            }

            if (mainWindow.Context.Authors.Where(a => a.Name == (string)value).Count() != 0)
            {
                return new ValidationResult(false, "Author already in a database");
            }

            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// Interaction logic for AddAuthorDialog.xaml
    /// </summary>
    public partial class AddAuthorDialog : Window
    {
        private MainWindow mainWindow;

        public String AuthorName { get; set; }

        public AddAuthorDialog(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            var binding = BindingOperations.GetBinding(this.AuthorTextBox, TextBox.TextProperty);
            var validation = new AuthorNameValidation(mainWindow);
            binding.ValidationRules.Add(validation);
        }

        private void button_add_Clicked(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }
    }
}
