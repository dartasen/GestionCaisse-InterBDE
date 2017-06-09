﻿using System;

namespace GestionCaisse_MVVM.Exceptions
{
    public class ConnectionFailedException : Exception
    {
        public ConnectionFailedException()
        {
        }

        public ConnectionFailedException(string message) : base(message)
        {
        }

        public ConnectionFailedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}