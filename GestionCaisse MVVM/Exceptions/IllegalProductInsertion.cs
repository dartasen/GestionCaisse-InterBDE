using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaisse_MVVM.Exceptions
{
    public class IllegalProductInsertion : Exception
    {
        public IllegalProductInsertion()
        {
        }

        public IllegalProductInsertion(string message) : base(message)
        {
        }
    }
}
