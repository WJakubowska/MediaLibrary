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
    /// <summary>
    /// Class responsible for validation the author's name 
    /// </summary>
    public class AuthorNameValidation : ValidationRule
    {
        private MainWindow mainWindow;

        /// <summary>
        /// TU POWINIEN BYĆ OPIS 
        /// </summary>
        /// <param name="mainWindow"> The main window of the application </param>
        public AuthorNameValidation(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// TU POWINIEN BYĆ OPIS 
        /// </summary>
        /// <param name="value"> TU POWINIEN BYĆ OPIS </param>
        /// <param name="cultureInfo">TU POWINIEN BYĆ OPIS </param>
        /// <returns>The result of the validation </returns>
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

        /// <summary>
        /// Gets or sets author's name 
        /// </summary>
        public String AuthorName { get; set; }

        /// <summary>
        /// Method adds a new author dialog
        /// </summary>
        /// <param name="mainWindow">The main window of the application</param>
        public AddAuthorDialog(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            var binding = BindingOperations.GetBinding(this.AuthorTextBox, TextBox.TextProperty);
            var validation = new AuthorNameValidation(mainWindow);
            binding.ValidationRules.Add(validation);
        }

        /// <summary>
        /// If the "Add" button is pressed then the method is executed.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
        private void button_add_Clicked(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }
    }
}
