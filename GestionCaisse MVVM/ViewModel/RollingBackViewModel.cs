using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class RollingBackViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly LoginService _loginService;

        public RollingBackViewModel()
        {
            _loginService = LoginService.Instance;

            CurrentUser = _loginService.GetLoginContext().User;

            DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTo = DateTime.Now;

            UpdateHistory();

            Quit = new RelayCommand(() => Close(), o => true);

            SaveChanges = new RelayCommand(() =>
            {
                var dialogService = new DialogService();
                try
                {
                    if (SelectedHistoryQueryResult == null)
                    {
                        dialogService.ShowInformationModern("Vous devez sélectionner un item !",
                                "Erreur de suppression");

                        return;
                    }

                    MessageBoxResult result = dialogService.ShowInformationWindow($"Voulez-vous vraiment supprimer cette vente ({SelectedHistoryQueryResult.IdVente}) ?",
                        "Confirmation de l'opération",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        if (!ProductService.RollBackSell(SelectedHistoryQueryResult.IdVente))
                        {
                            dialogService.ShowInformationModern("Erreur lors de la suppression",
                                "Erreur de suppression");
                            return;
                        }

                        dialogService.ShowInformationModern("Vous venez de supprimer la vente n°" + SelectedHistoryQueryResult.IdVente,
                                 "Confirmation de suppression vente");

                        UpdateHistory();
                        OnPropertyChanged(nameof(History));
                    }
                }
                catch (Exception ex)
                {
                    dialogService.ShowInformationModern("Erreur :\n" + ex.InnerException.Message,
                    "Mise à jour impossible");
                }
            }, o => true);
        }

        #region Properties

        private List<QueryHistory> _history;

        public List<QueryHistory> History
        {
            get => _history;
            set { _history = value; OnPropertyChanged(); }
        }

        private QueryHistory _selectedHistoryQueryResult;

        public QueryHistory SelectedHistoryQueryResult
        {
            get => _selectedHistoryQueryResult;
            set { _selectedHistoryQueryResult = value; OnPropertyChanged(); }
        }

        public IEnumerable<User> Users => UserService.GetUsers();

        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); UpdateHistory();}
        }

        private DateTime _dateFrom;

        public DateTime DateFrom
        {
            get => _dateFrom;
            set { _dateFrom = value; OnPropertyChanged(); UpdateHistory(); }
        }

        private DateTime _dateTo;

        public DateTime DateTo
        {
            get => _dateTo;
            set { _dateTo = value; OnPropertyChanged(); UpdateHistory(); }
        }

        public bool IsAdmin => _loginService.GetLoginContext().User.IsAdmin;

        public string Title => !CurrentUser.IsAdmin ? "Rolling Back sur la journée" : "Rolling Back";
        #endregion

        #region Commands

        public ICommand Quit { get; set; }
        public ICommand SaveChanges { get; set; }
        #endregion

        private void UpdateHistory()
        {
            _history = ProductService.GetHistory(_dateFrom, _dateTo, _currentUser.IdUtilisateur).OrderByDescending(x => x.IdVente).ToList();
            OnPropertyChanged(nameof(History));
        }

        private void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
    }
}
