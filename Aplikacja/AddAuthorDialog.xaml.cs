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
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string) value))
            {
                return new ValidationResult(false, "Author name can not be empty");
            }

            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// Interaction logic for AddAuthorDialog.xaml
    /// </summary>
    public partial class AddAuthorDialog : Window
    {
        public String AuthorName { get; set; }

        public AddAuthorDialog()
        {
            InitializeComponent();
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
