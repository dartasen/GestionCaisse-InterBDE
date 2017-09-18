using System;
using System.Reflection;
using System.Windows;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.View;

namespace GestionCaisse_MVVM.Model.Services
{
    public class DialogService
    {
        /// <summary>
        ///     Display ProductInsertionView
        /// </summary>
        public void ShowProductInsertPage()
        {
            var createType = Type.GetType("GestionCaisse_MVVM.View.ProductInsertionView, GestionCaisse");
            var window = (Window)Activator.CreateInstance(createType);

            window.ShowDialog();
        }

        /// <summary>
        ///     Display MainWindowView
        /// </summary>
        public void ShowMainWindow()
        {
            var createType = Type.GetType("GestionCaisse_MVVM.View.MainWindowView, GestionCaisse");

            try
            {
                var window = (Window)Activator.CreateInstance(createType);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationWindow(
                    "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                    "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Display AdministrationView
        /// </summary>
        public void ShowAdministrationWindow()
        {
            var createType = Type.GetType("GestionCaisse_MVVM.View.AdministrationView, GestionCaisse");

            try
            {
                var window = (Window)Activator.CreateInstance(createType);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationWindow(
                    "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                    "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Display RankingPlayersBySellsView
        /// </summary>
        public void ShowRankingWindow()
        {
            var createType = Type.GetType("GestionCaisse_MVVM.View.RankingPlayersBySellsView, GestionCaisse");

            try
            {
                var window = (Window)Activator.CreateInstance(createType);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationWindow(
                    "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                    "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Display RollingBackView
        /// </summary>
        public void ShowRollingBackWindow()
        {
            var createType = Type.GetType("GestionCaisse_MVVM.View.RollingBackView, GestionCaisse");

            try
            {
                var window = (Window)Activator.CreateInstance(createType);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationWindow(
                    "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                    "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        ///     Display CheckPasswordView
        /// </summary>
        public void ShowCheckPasswordView(string windowToOpen)
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(CheckPasswordView), windowToOpen);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationWindow(
                    "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                    "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Display AddUserView
        /// </summary>
        public void ShowAddUserView()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(AddUserView));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationWindow(
                    "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                    "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Display a quick MessageBox fully customizable
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="title">Title of the window</param>
        /// <param name="buttonType">Type of button to propose</param>
        /// <param name="imageType">Image type to display</param>
        public MessageBoxResult ShowInformationWindow(
            string message,
            string title = null,
            MessageBoxButton buttonType = MessageBoxButton.OK,
            MessageBoxImage imageType = MessageBoxImage.None)
        {
            return MessageBox.Show(message, title, buttonType, imageType);
        }
    }
}