using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionCaisse_MVVM.ViewModel.ClientCashing
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

        public int Passkey { get; set; }

        #region Commands
        public ICommand CheckPasskey { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(p));
    }
}
