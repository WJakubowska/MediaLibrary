using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aplikacja
{
    /// <summary>
    /// Class responsible for validation 
    /// </summary>
    public class MyValidator
    {
        /// <summary>
        /// TU POWINIEN BYĆ OPIS 
        /// </summary>
        /// <param name="node"> An object that participates in the dependency property system</param>
        /// <returns>The result of validation : true or false</returns>
        public static bool IsValid(DependencyObject node)
        {
            if (node != null)
            {
                if (Validation.GetHasError(node))
                {
                    if (node is IInputElement)
                    {
                        Keyboard.Focus((IInputElement)node);
                    }
                    return false;
                }
            }

            foreach (var subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (subnode is DependencyObject)
                {
                    if (!IsValid((DependencyObject)subnode))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
