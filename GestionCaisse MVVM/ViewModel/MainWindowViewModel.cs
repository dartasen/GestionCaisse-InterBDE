﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly BasketService _basketService = BasketService.Instance;
        private readonly LoginService _loginService = LoginService.Instance;

        public MainWindowViewModel()
        {
            var dialogService = new DialogService();

            UpdateUserSellsSmiley();

            InsertProduct = new RelayCommand(() =>
            {
                LoginService.Instance.IsTimerActive = false;
                dialogService.ShowProductInsertPage();
            }, o => true);

            ResetBasket = new RelayCommand(() => _basketService.GetBasket().ResetBasket(), o => true);
            DeleteBasketProduct = new RelayCommand(() => _basketService.GetBasket().RemoveProduct(_currentBasketProduct), o => true);
            ShowAdministrationWindow = new RelayCommand(() =>
            {
                LoginService.Instance.IsTimerActive = false;
                dialogService.ShowCheckPasswordView("administration");
                LoginService.Instance.IsTimerActive = true;
            }, o =>
            {
                return _loginService.GetLoginContext().User.IsAdmin;
            });

            ShowRollingBackWindow = new RelayCommand(() =>
            {
                LoginService.Instance.IsTimerActive = false;
                dialogService.ShowCheckPasswordView("rollingback");
                LoginService.Instance.IsTimerActive = true;
                UpdateUserSellsSmiley();
            }, o => true);

            ValidateClientCashing = new RelayCommand(() =>
            {
                dialogService.ShowClientCashing();
            }, o => true);

            ValidateSell = new RelayCommand(() =>
            {
                try
                {
                    if (_basketService.ValidateSell())
                    {
                        _basketService.GetBasket().ResetBasket();
                        dialogService.ShowInformationWindow(
                            "Vente effectuée !",
                            "Confirmation",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);

                        dialogService.ShowInformationModern("Vente effectuée !",
                            "Confirmation de vente");

                        UpdateUserSellsSmiley();
                    }
                    else
                    {
                        dialogService.ShowInformationModern("Vente invalide ou impossible ! ",
                            "Erreur de vente");
                    }
                }
                catch (ConnectionFailedException ex)
                {
                    if (ex.InnerException != null)
                        dialogService.ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
                }
            }, o => true);

            IncreaseSelectedProductQuantity = new RelayCommand(() =>
            {
                if (_currentBasketProduct == null) return;
                _basketService.GetBasket().IncreaseQuantity(_currentBasketProduct);
            }, o => true);

            DecreaseSelectedProductQuantity = new RelayCommand(() =>
            {
                if (_currentBasketProduct == null) return;
                _basketService.GetBasket().DecreaseQuantity(_currentBasketProduct);
            }, o => true);

            RefreshSessionDelay = new RelayCommand(() =>
            {
                _timer = _loginService.GetLoginContext().User.IsAdmin ? AppInformations.DefaultSessionDelayForSuperusers : AppInformations.DefaultSessionDelay;
                OnPropertyChanged(nameof(Countdown));
            }, o => true);

            Logout = new RelayCommand(() =>
            {
                _basketService.GetBasket().ResetBasket();
                try
                {
                    Timer.Stop();
                }
                catch { }
                LoginService.ShowLoginWindow();
                Close();
            }, o => true);

            _timer = _loginService.GetLoginContext().User.IsAdmin ? AppInformations.DefaultSessionDelayForSuperusers : AppInformations.DefaultSessionDelay;
            StartTimer();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        #region Commands

        public ICommand ResetBasket { get; }

        public ICommand InsertProduct { get; }

        public ICommand ValidateSell { get; }

        public ICommand ValidateClientCashing { get; set; }

        public ICommand Logout { get; }

        public ICommand DeleteBasketProduct { get; }

        public ICommand ShowAdministrationWindow { get; }

        public ICommand ShowRollingBackWindow { get; }

        public ICommand RefreshSessionDelay { get; }

        public ICommand IncreaseSelectedProductQuantity { get; }

        public ICommand DecreaseSelectedProductQuantity { get; }
        #endregion

        #region Properties

        public Basket Basket => BasketService.Instance.GetBasket();

        public IEnumerable<BDE> BDEs
        {
            get
            {
                var bdes = Enumerable.Empty<BDE>();

                try
                {
                    bdes = BDEService.GetBDEs();
                }
                catch (ConnectionFailedException ex)
                {
                    var dialogService = new DialogService();
                    dialogService.ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
                }

                return bdes;
            }
        }

        private BasketProduct _currentBasketProduct;

        public BasketProduct CurrentBasketProduct
        {
            get => _currentBasketProduct;
            set => _currentBasketProduct = value;
        }

        private BDE _selectedBDE;

        public BDE SelectedBDE
        {
            get => _selectedBDE;
            set
            {
                _selectedBDE = value;
                _loginService.GetLoginContext().BuyingBDE = value;
                OnPropertyChanged();
            }
        }

        public string CurrentUser => $"{_loginService.GetLoginContext().User.Nom} ({_loginService.GetLoginContext().BuyingBDE.Nom})";

        private DispatcherTimer Timer;
        private int _timer;

        public string Countdown
        {
            get
            {
                var timespan = TimeSpan.FromSeconds(_timer);
                return timespan.ToString(@"hh\:mm\:ss");
            }
        }

        private int _sellsMadeToday;

        public int SellsMadeToday
        {
            get { return _sellsMadeToday; }
            set { _sellsMadeToday = value; OnPropertyChanged(); }
        }
        public IValueConverter ValueConverter { get; set; }

        #endregion

        private void UpdateUserSellsSmiley()
        {
            _sellsMadeToday = UserService.SellsMadeByUserToday(_loginService.GetLoginContext().User);
            OnPropertyChanged(nameof(SellsMadeToday));
        }

        private void StartTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            LoginService.Instance.IsTimerActive = true;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (LoginService.Instance.IsTimerActive)
            {
                if (_timer > 0)
                {
                    _timer--;
                    OnPropertyChanged(nameof(Countdown));
                }
                else
                {
                    DialogService dialogService = new DialogService();
                    Hide();
                    dialogService.ShowInformationWindow("La session a expiré, reconnectez-vous !", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Logout.Execute(null);
                }
            }
        }
    }
}