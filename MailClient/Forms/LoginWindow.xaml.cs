using AutoMapper;
using MailClient.DAL.Entities;
using MailClient.IMap.Models;
using MailClient.Proxy.Interfaces;
using MailClient.Singletons;
using System;
using System.Windows;

namespace MailClient.Forms
{
    public partial class LoginWindow : Window
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkProxy _proxy;

        public LoginWindow()
        {
            InitializeComponent();
            _mapper = AutoMapperProvider.GetIMapper();
            _proxy = ProxyProvider.GetProxy();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            bool isSucseeded = TryToLogin(
                                    HostnameInput.Text,
                                    LoginInput.Text,
                                    PasswordInput.Password.ToString());

            if (isSucseeded)
            {
                AddUserToDatabase(HostnameInput.Text, LoginInput.Text);
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
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void AddUserToDatabase(string hostname, string username)
        {
            ApplicationUser user = GetUserModel(hostname, username);
            _proxy.UserRepository.Add(_mapper.Map<ApplicationUser, User>(user));
        }

        private ApplicationUser GetUserModel(string hostname, string username)
        {
            return new ApplicationUser
            {
                Username = username,
                Hostname = hostname
            };
        }
    }
}
