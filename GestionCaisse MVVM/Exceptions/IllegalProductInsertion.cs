using System;

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
