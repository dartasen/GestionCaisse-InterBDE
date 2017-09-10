using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaisse_MVVM.Model.Services
{
    public class AppInformations
    {
        public static readonly string Version = "1.1";
        public static readonly int DefaultSessionDelay = 300; //in seconds
        public static readonly int DefaultSessionDelayForSuperusers = 900; //in seconds
    }
}
