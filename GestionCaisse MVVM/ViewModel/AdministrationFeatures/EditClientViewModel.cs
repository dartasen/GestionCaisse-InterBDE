using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class EditClientViewModel : ViewModelBase, INotifyPropertyChanged
    {

        //Edit a client
        public EditClientViewModel(Client client)
        {
            WindowName = "Edition d'un client";

            IsAddClientVisible = false;
            IsValidateChangesVisible = true;

            NewClient = new Client
            {
                Nom = client.Nom,
                IdCarte = client.IdCarte,
                CodeSecret = client.CodeSecret,
                IdBde = client.IdBde,
                Credit = client.Credit
            };

            BDEs = new List<BDE>(BDEService.GetBDEs());

            SelectedBde = BDEs.FirstOrDefault(x => x.Id == client.IdBde);

            Quit = new RelayCommand(() => Close(), o => true);

            GenerateNewPasskey = new RelayCommand(() =>
            {
                Passkey = ClientService.GenerateNewPasskey();
            }, o => true);

            ValidateChanges = new RelayCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(BadgeID) || string.IsNullOrWhiteSpace(BadgeID) || BadgeID.Length > 15 ||
                    SelectedBde == null || double.IsNaN(Balance) || Balance < 0.00)
                {
                    FormNotValid();
                    return;
                }

                NewClient.IdBde = SelectedBde.Id;
                ClientService.ValidateChanges(client, NewClient);

                var dialogService = new DialogService();
                dialogService.ShowInformationModern("Le compte client a correctement été mis à jour !", "Modifications effectuées");

                Close();
            }, o => true);
        }

        //Add a new client
        public EditClientViewModel()
        {
            WindowName = "Ajout d'un client";

            IsAddClientVisible = true;
            IsValidateChangesVisible = false;

            NewClient = new Client();

            Passkey = ClientService.GenerateNewPasskey();

            BDEs = new List<BDE>(BDEService.GetBDEs());

            SelectedBde = BDEs.FirstOrDefault();

            Quit = new RelayCommand(() => Close(), o => true);

            GenerateNewPasskey = new RelayCommand(() =>
            {
                Passkey = ClientService.GenerateNewPasskey();
            }, o => true);

            AddClient = new RelayCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(BadgeID) || string.IsNullOrWhiteSpace(BadgeID) || BadgeID.Length > 15 ||
                    SelectedBde == null || double.IsNaN(Balance) || Balance < 0.00)
                {
                    FormNotValid();
                    return;
                }

                NewClient.IdBde = SelectedBde.Id;
                ClientService.AddClient(NewClient);

                var dialogService = new DialogService();
                dialogService.ShowInformationModern("Le compte client a correctement été ajouté !", "Ajout effectué");

                Close();
            }, o => true);
        }

        #region Properties
        public string WindowName { get; }

        private Client _newClient;

        public Client NewClient
        {
            get => _newClient;
            set { _newClient = value; OnPropertyChanged(); }
        }

        private List<BDE> _bdes;

        public List<BDE> BDEs
        {
            get => _bdes;
            set { _bdes = value; }
        }

        private BDE _selectedBde;

        public BDE SelectedBde
        {
            get => _selectedBde;
            set { _selectedBde = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => NewClient.Nom;
            set { NewClient.Nom = value; OnPropertyChanged(); }
        }

        public string BadgeID
        {
            get => NewClient.IdCarte;
            set { NewClient.IdCarte = value; OnPropertyChanged(); }
        }

        public int Passkey
        {
            get => NewClient.CodeSecret;
            set { NewClient.CodeSecret = value; OnPropertyChanged(); }
        }

        public double Balance
        {
            get => NewClient.Credit;
            set { NewClient.Credit = value; OnPropertyChanged(); }
        }

        private bool _isValidateChangesVisible;

        public bool IsValidateChangesVisible
        {
            get => _isValidateChangesVisible;
            set { _isValidateChangesVisible = value; OnPropertyChanged(); }
        }

        private bool _isAddClientVisible;

        public bool IsAddClientVisible
        {
            get => _isAddClientVisible;
            set { _isAddClientVisible = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands
        public ICommand Quit { get; set; }
        public ICommand GenerateNewPasskey { get; set; }
        public ICommand ValidateChanges { get; set; }
        public ICommand AddClient { get; set; }
        #endregion

        private void FormNotValid()
        {
            var dialogService = new DialogService();
            dialogService.ShowInformationModern("Tous les champs doivent-êtres valides !", "Ajout impossible");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
