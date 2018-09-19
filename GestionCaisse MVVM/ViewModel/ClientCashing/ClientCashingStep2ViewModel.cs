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

                        if (client.Credit < totalPrice)
                        {
                            dialogService.ShowInformationModern($"Ce compte ne possède pas assez de crédit ! ({client.Credit} sur le compte pour un total de {totalPrice}",
                                "Achat impossible");

                            return;
                        }
                        
                        if (basketService.ValidateSell(_client.IdClient))
                        {
                            ClientService.ChargeClient(_client, totalPrice);
                            basketService.GetBasket().ResetBasket();
                            dialogService.ShowInformationModern("Vente effectuée !", "Confirmation Vente");

                            _closeWindow.Invoke();
                        }
                        else
                        {
                            dialogService.ShowInformationModern("Vente invalide ou impossible !", "Erreur Vente");
                        }
                    }
                    catch (ConnectionFailedException ex)
                    {
                            dialogService.ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                                    "Erreur de connexion");
                    }

                },
            o => true);
        }

        #region Properties

        public string Name => _client.Nom;
        public string BadgeID => _client.IdCarte;
        public string Balance => _client.Credit + " €";
        #endregion

        #region Commands
        public ICommand ValidateSell { get; set; }
        public ICommand GoBackward { get; set; }
        #endregion
    }
}
