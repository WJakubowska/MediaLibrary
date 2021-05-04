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
    /// Class validating directory passed by user
    /// </summary>
    public class AuthorNameValidation : ValidationRule
    {
        private IAuthorUniquenessChecker _authorUniquenessChecker;

        /// <summary>
        /// Ctor of AuthorNameValidation
        /// </summary>
        /// <param name="authorUniquenessChecker"> Object that lets us check if author name is unique </param>
        public AuthorNameValidation(IAuthorUniquenessChecker authorUniquenessChecker)
        {
            this._authorUniquenessChecker = authorUniquenessChecker;
        }

        /// <summary>
        /// This validates name of an author as passed from the user.
        /// </summary>
        /// <param name="value"> This is value of  </param>
        /// <param name="cultureInfo">  </param>
        /// <returns>The result of the validation </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string value_string = value as string;

            if (value_string == null)
            {
                return new ValidationResult(false, "Author name not a string");
            }

            if (String.IsNullOrEmpty(value_string))
            {
                return new ValidationResult(false, "Author name can not be empty");
            }

            if (!_authorUniquenessChecker.IsAuthorNameUnique(value_string))
            {
                return new ValidationResult(false, "Author already in a database");
            }

            if (value_string.Length < 3)
            {
                return new ValidationResult(false, "Author name can't be less than 3 char");
            }

            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Interaction logic for AddAuthorDialog
    /// </summary>
    public partial class AddAuthorDialog : Window
    {
        /// <summary>
        /// Gets or sets author's name
        /// </summary>
        public String AuthorName { get; set; }

        /// <summary>
        /// Ctor of AddAuthorDialog
        /// </summary>
        /// <param name="mainWindow">The main window of the application</param>
        /// <param name="authorUniquenessChecker">Object for checking author name uniqueness</param>
        public AddAuthorDialog(MainWindow mainWindow, IAuthorUniquenessChecker authorUniquenessChecker)
        {
            InitializeComponent();

            var binding = BindingOperations.GetBinding(this.AuthorTextBox, TextBox.TextProperty);
            var validation = new AuthorNameValidation(authorUniquenessChecker);
            binding.ValidationRules.Add(validation);
        }

        /// <summary>
        /// If the "Add" button is pressed then the method is executed.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void button_add_Clicked(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }
    }
}
