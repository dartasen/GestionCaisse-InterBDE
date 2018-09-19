using System;
using System.Linq;
using System.Windows;
using GestionCaisse_MVVM.View;
using GestionCaisse_MVVM.View.AdministrationFeatures;
using GestionCaisse_MVVM.View.ClientCashing;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GestionCaisse_MVVM.Model.Services
{
    public class DialogService
    {
        /// <summary>
        ///     Display ProductInsertionView
        /// </summary>
        public void ShowProductInsertPage()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(ProductInsertionView));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///     Display MainWindowView
        /// </summary>
        public void ShowMainWindow()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(MainWindowView));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///     Display ClientCashing
        /// </summary>
        public void ShowClientCashing()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(ClientCashing));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///     Display AdministrationView
        /// </summary>
        public void ShowAdministrationWindow()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(AdministrationView));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///     Display EditClientView
        /// </summary>
        public void ShowEditClient(Client client)
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(EditClientView), client);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///     Display EditClientView in Add mode
        /// </summary>
        public void ShowAddClient()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(EditClientView));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///     Display RollingBackView
        /// </summary>
        public void ShowRollingBackWindow()
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(RollingBackView));
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
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
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
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
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
            }
        }

        /// <summary>
        ///  Display ChangePasswordView
        /// </summary>
        /// <param name="user">user to modify the password</param>
        public void ShowChangePasswordView(User user)
        {
            try
            {
                var window = (Window)Activator.CreateInstance(typeof(ChangePasswordView), user);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
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

        public void ShowInformationModern(string message, string title = null, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            try
            {
                var window = (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MetroWindow);
                window.ShowMessageAsync(title, message, style);
            } catch {
                ShowInformationWindow(title, message);
            }
        }
    }
}