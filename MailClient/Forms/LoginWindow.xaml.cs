using AutoMapper;
using MailClient.IMap.Interfaces;
using MailClient.Singletons;
using System;
using System.Windows;

namespace MailClient.Forms
{
    public partial class LoginWindow : Window
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public LoginWindow()
        {
            InitializeComponent();
            _mapper = AutoMapperProvider.GetIMapper();
            _clientService = ApplicationProvider.GetProxy();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            bool isSucseeded = TryToLogin(
                                    HostnameInput.Text,
                                    LoginInput.Text,
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
                _clientService.Login(hostname, username, password);
                _clientService.SwitchAccount(username);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
    }
}
