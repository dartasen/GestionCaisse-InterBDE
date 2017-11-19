using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.View;

namespace GestionCaisse_MVVM.ViewModel
{
    public class ClientCashingViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private UserControl[] _steps;

        public ClientCashingViewModel()
        {
            _steps = new UserControl[2];
            _steps[0] = new ClientCashingStep1UserControl(CheckPasskey);

            CurrentUserControl = _steps[0];
        }

        private void CheckPasskey(int passkey)
        {
            DialogService dialogService = new DialogService();
            Client c = ClientService.CheckPassKey(passkey);

            if (c == null)
            {
                dialogService.ShowInformationWindow("Je ne connais pas cet client !", "Client inconnu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _steps[1] = new ClientCashingStep2UserControl(c, GoBackward, Close);
                CurrentUserControl = _steps[1];
            }

        }

        private void GoBackward()
        {
            CurrentUserControl = _steps[0];
        }

        #region Properties
        private UserControl _currentUserControl;

        public UserControl CurrentUserControl
        {
            get { return _currentUserControl; }
            set { _currentUserControl = value; OnPropertyChanged();}
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(p));
    }
}
