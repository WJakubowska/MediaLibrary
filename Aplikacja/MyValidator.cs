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
    /// Class for generalizing processing validator
    /// </summary>
    public class MyValidator
    {
        /// <summary>
        /// Checks all validators for an DependencyObject
        /// </summary>
        /// <param name="node"> An object that participates in the dependency property system</param>
        /// <returns>The result of validation</returns>
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
