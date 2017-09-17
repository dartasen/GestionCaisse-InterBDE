using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
