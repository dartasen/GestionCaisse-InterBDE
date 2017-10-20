using System;

namespace GestionCaisse_MVVM.Model.Entities
{
    [Flags]
    public enum ConnectionResult
    {
        NotFound,
        Authorized,
        Disabled
    }
}
