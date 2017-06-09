using GestionCaisse_MVVM.ViewModel;
using System.Configuration;
using System.Windows;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            EncryptConfigSection("GestionCaisse.exe");

            var vm = new LoginViewModel()
            {
                Close = () => this.Close(),
                Show = () => this.Show(),
                Hide = () => this.Hide()
            };

            DataContext = vm;

            usernameTextBox.Focus();
        }

        /// <summary>
        /// Encrypte le connectionString d'une section de App.config
        /// </summary>
        /// <param name="appName">Le nom du fichier de l'application</param>
        /// <example>EncryptConfigSection(MonApplication.exe);</example>
        private void EncryptConfigSection(string appName)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(appName);
                ConnectionStringsSection section = (ConnectionStringsSection)config.GetSection("connectionStrings");

                if (!section.SectionInformation.IsProtected)
                {
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    config.Save();
                }
            }
            catch
            {
                MessageBox.Show("Impossible de lire le fichier de configuration !", "Fichier de configuration invalide !", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
    }
}
