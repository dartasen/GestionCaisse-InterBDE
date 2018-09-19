using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class ClientManagementViewModel : INotifyPropertyChanged
    {

        public ClientManagementViewModel()
        {
            var dialogService = new DialogService();

            EditClient = new RelayCommand(() =>
            {
                if (SelectedClient == null)
                {
                    dialogService.ShowInformationModern("Merci de sélectionner un client", "Opération impossible");
                    return;
                }

                dialogService.ShowEditClient(SelectedClient);

                OnPropertyChanged(nameof(Clients));
            }, o => true);

            RemoveClient = new RelayCommand(() =>
            {
                if (SelectedClient == null)
                {
                    dialogService.ShowInformationModern("Merci de sélectionner un client", "Opération impossible");
                    return;
                }

                var result = dialogService.ShowInformationWindow(
                    "Voulez-vous vraiment supprimer ce client ?",
                    "Confirmation de l'opération",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (!result.Equals(MessageBoxResult.Yes)) return;

                try
                {
                    ClientService.RemoveClient(SelectedClient);
                    dialogService.ShowInformationModern("Le client a bien été supprimé du système", "Opération réussie");
                }
                catch (Exception ex)
                {
                    dialogService.ShowInformationModern("Erreur lors de la suppression " + ex.InnerException.Message, "Opération impossible");
                }
                OnPropertyChanged(nameof(Clients));
            }, o => true);

            AddClient = new RelayCommand(() =>
            {
                dialogService.ShowAddClient();

                OnPropertyChanged(nameof(Clients));
            }, o => true);
        }

        #region Properties

        public IEnumerable<Client> Clients => ClientService.GetClients();

        private Client _selectedClient;

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; }
        }

        #endregion

        #region Commands
        public ICommand EditClient { get; set; }
        public ICommand AddClient { get; set; }
        public ICommand RemoveClient { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
