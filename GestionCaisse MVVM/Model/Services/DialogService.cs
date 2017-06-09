﻿using System;
using System.Windows;

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
            var window = (Window) Activator.CreateInstance(createType);

            window.ShowDialog();
        }

        /// <summary>
        ///     Display MainWindowView
        /// </summary>
        public void ShowMainWindow()
        {
            var createType = Type.GetType("GestionCaisse_MVVM.View.MainWindowView, GestionCaisse");
            var window = (Window) Activator.CreateInstance(createType);

            window.ShowDialog();
        }

        /// <summary>
        ///     Display a quick MessageBox fully customizable
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="title">Title of the window</param>
        /// <param name="buttonType">Type of button to propose</param>
        /// <param name="imageType">Image type to display</param>
        public void ShowInformationWindow(
            string message,
            string title = null,
            MessageBoxButton buttonType = MessageBoxButton.OK,
            MessageBoxImage imageType = MessageBoxImage.None)
        {
            MessageBox.Show(message, title, buttonType, imageType);
        }
    }
}