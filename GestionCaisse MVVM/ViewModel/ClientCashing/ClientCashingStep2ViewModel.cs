using System;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.ClientCashing
{
    public class ClientCashingStep2ViewModel
    {
        private readonly Client _client;
        private readonly Action _goBackward;
        private readonly Action _closeWindow;

        public ClientCashingStep2ViewModel(Client client, Action goBackward, Action closeWindow)
        {
            _client = client;
            _goBackward = goBackward;
            _closeWindow = closeWindow;

            GoBackward = new RelayCommand(() => _goBackward.Invoke(), o => true);

            ValidateSell = new RelayCommand(() =>
                {
                    DialogService dialogService = new DialogService();

                    try
                    {
                        var basketService = BasketService.Instance;

                        var totalPrice = basketService.GetBasket().TotalPrice;

                        if (client.Balance < totalPrice)
                        {
                            dialogService.ShowInformationWindow(
                                $"Ce compte ne possède pas assez de crédit ! ({client.Balance} sur le compte pour un total de {totalPrice}",
                                "Achat impossible", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        
                        if (basketService.ValidateSell(_client.IdClient))
                        {
                            ClientService.ChargeClient(_client, totalPrice);
                            basketService.GetBasket().ResetBasket();
                            dialogService.ShowInformationWindow(
                                "Vente effectuée !",
                                "Confirmation",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                            _closeWindow.Invoke();
                        }
                        else
                        {
                            dialogService.ShowInformationWindow(
                                "Vente invalide ou impossible !",
                                "Attention",
                                MessageBoxButton.OK,
                                MessageBoxImage.Hand);
                        }
                    }
                    catch (ConnectionFailedException ex)
                    {
                        if (ex.InnerException != null)
                            dialogService.ShowInformationWindow(
                                "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                                "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                },
            o => true);
        }

        #region Properties

        public string Name => _client.Name;
        public string BadgeID => _client.BadgeID;
        public string Balance => _client.Balance + " €";
        #endregion

        #region Commands
        public ICommand ValidateSell { get; set; }
        public ICommand GoBackward { get; set; }
        #endregion
    }
}
