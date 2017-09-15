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

namespace GestionCaisse_MVVM.ViewModel
{
    public class RollingBackViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private LoginService _loginService;

        public RollingBackViewModel()
        {
            _loginService = LoginService.Instance;
            UpdateHistory();

            Quit = new RelayCommand(() => Close(), o => true);

            SaveChanges = new RelayCommand(() =>
            {
                var dialogService = new DialogService();
                try
                {
                    if (SelectedHistoryQueryResult == null)
                    {
                        dialogService.ShowInformationWindow("Vous devez sélectionner un item !", "Erreur de suppresion",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MessageBoxResult result = dialogService.ShowInformationWindow($"Voulez-vous vraiment supprimer cette vente ({SelectedHistoryQueryResult.IdSale}) ?",
                        "Confirmation de l'opération",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        if (!ProductService.RollBackSell(SelectedHistoryQueryResult.IdSale))
                        {
                            dialogService.ShowInformationWindow("Erreur lors de la suppression !", "Erreur de suppresion",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        dialogService.ShowInformationWindow("Vous venez de supprimer la vente n°" + SelectedHistoryQueryResult.IdSale, "Vente supprimée !",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        UpdateHistory();
                        OnPropertyChanged(nameof(History));
                    }
                }
                catch (Exception e)
                {
                    dialogService.ShowInformationWindow("Erreur :\n" + e, "Mise à jour impossible !", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }, o => true);
        }

        #region Properties

        private List<ProductService.HistoryQueryResult> _history;

        public List<ProductService.HistoryQueryResult> History
        {
            get { return _history; }
            set { _history = value; OnPropertyChanged(); }
        }

        private ProductService.HistoryQueryResult _selectedHistoryQueryResult;

        public ProductService.HistoryQueryResult SelectedHistoryQueryResult
        {
            get { return _selectedHistoryQueryResult; }
            set { _selectedHistoryQueryResult = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand Quit { get; set; }
        public ICommand SaveChanges { get; set; }
        #endregion

        private void UpdateHistory() => _history = ProductService.GetHistory(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), DateTime.Now, _loginService.GetLoginContext().User.IdUser);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
