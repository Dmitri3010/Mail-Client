using MailClient.IMap.Models;
using MailClient.Proxy.Interfaces;
using MailClient.Singletons;
using System;
using System.Windows;


namespace MailClient.Forms
{
    public partial class EnterPasswordWindow : Window
    {
        private readonly IUnitOfWorkProxy _proxy;
        private ApplicationUser _user;

        public EnterPasswordWindow(ApplicationUser user)
        {
            InitializeComponent();
            _user = user;
            _proxy = ProxyProvider.GetProxy();

            HostameLable.Content = _user.Hostname;
            UsernameLable.Content = _user.Username;
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            bool isSucseeded = TryToLogin(
                                    _user.Hostname,
                                    _user.Username,
                                    PasswordInput.Password.ToString());

            if (isSucseeded)
            {
                Close();
            }
        }

        private bool TryToLogin(string hostname, string username, string password)
        {
            try
            {
                _proxy.MessageRepositoryProxy.Login(hostname, username, password);
                _proxy.MessageRepositoryProxy.SwitchAccount(username);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
