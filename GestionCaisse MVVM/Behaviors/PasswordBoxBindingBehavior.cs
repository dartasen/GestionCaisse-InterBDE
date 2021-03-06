﻿using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace GestionCaisse_MVVM.Behaviors
{
    /// <summary>
    ///     Set PasswordBox's behavior in LoginView in order to use the SecureString provided
    ///     by the PasswordBox as we're using MVVM and it isn't available through classic binding
    /// </summary>
    public class PasswordBoxBindingBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(PasswordBoxBindingBehavior),
                new PropertyMetadata(OnSourcePropertyChanged));

        public SecureString Password
        {
            get => (SecureString) GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += OnPasswordBoxValueChanged;
        }

        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                var behavior = d as PasswordBoxBindingBehavior;
                behavior.AssociatedObject.PasswordChanged -= OnPasswordBoxValueChanged;
                behavior.AssociatedObject.Password = string.Empty;
                behavior.AssociatedObject.PasswordChanged += OnPasswordBoxValueChanged;
            }
        }

        private static void OnPasswordBoxValueChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var behavior = Interaction.GetBehaviors(passwordBox).OfType<PasswordBoxBindingBehavior>().FirstOrDefault();
            if (behavior != null)
            {
                var binding = BindingOperations.GetBindingExpression(behavior, PasswordProperty);
                if (binding != null)
                {
                    var property = binding.DataItem.GetType().GetProperty(binding.ParentBinding.Path.Path);
                    if (property != null)
                        property.SetValue(binding.DataItem, passwordBox.SecurePassword, null);
                }
            }
        }
    }
}