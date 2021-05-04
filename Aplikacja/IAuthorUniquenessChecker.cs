using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja
{
    public interface IAuthorUniquenessChecker
    {
        bool IsAuthorNameUnique(string authorName);
    }
}
