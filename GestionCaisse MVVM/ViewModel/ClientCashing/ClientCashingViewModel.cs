using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.View.ClientCashing;

namespace GestionCaisse_MVVM.ViewModel.ClientCashing
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
