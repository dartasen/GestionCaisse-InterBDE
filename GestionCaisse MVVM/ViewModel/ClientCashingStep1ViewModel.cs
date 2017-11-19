using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionCaisse_MVVM.ViewModel
{
    public class ClientCashingStep1ViewModel : INotifyPropertyChanged
    {
        private readonly Action<int> _checkPasskey;

        public ClientCashingStep1ViewModel(Action<int> checkPasskey)
        {
            _checkPasskey = checkPasskey;

            CheckPasskey = new RelayCommand(() =>
            {
                _checkPasskey.Invoke(Passkey);
            }, o => true);
        }


        #region Properties
        private int _passkey;

        public int Passkey
        {
            get { return _passkey; }
            set { _passkey = value; }
        }

        #endregion

        #region Commands
        public ICommand CheckPasskey { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(p));
    }
}
