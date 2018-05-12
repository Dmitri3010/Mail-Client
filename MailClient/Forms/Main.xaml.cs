using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using MailClient.Singletons;
using AutoMapper;
using System.Threading.Tasks;
using MailClient.Models;
using MailClient.DAL.Entities;
using mail.Forms;
using MailClient.Proxy.Interfaces;
using MailClient.IMap.Models;

namespace MailClient.Forms
{
    public partial class Main : Window
    {
        private IMapper _mapper;
        private IUnitOfWorkProxy _proxy;

        private List<MessageModel> messages;

        public Main()
        {
            InitializeComponent();

            _mapper = AutoMapperProvider.GetIMapper();
            _proxy = ProxyProvider.GetProxy();

            Show();
            SetIcons();
            Configure();
        }

        private void Configure()
        {
            if (!_proxy.MessageRepositoryProxy.IsAnyUserLoggedIn())
            {
                CheckForUsersInDatabaseAndOpenHandlerWindow();
                return;
            }
        }

        private void CheckForUsersInDatabaseAndOpenHandlerWindow()
        {
            IEnumerable<User> users = _proxy.UserRepository.GetAll();
            if (users.Count() == 0)
            {
                OpenLoginWindow();
            }
            else
            {
                OpenEnterPasswordWindow(_mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(users));
            }
        }

        private void OpenLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();
        }

        private void OpenEnterPasswordWindow(IEnumerable<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                EnterPasswordWindow enterPasswordWindow = new EnterPasswordWindow(user);
                enterPasswordWindow.Owner = this;
                enterPasswordWindow.ShowDialog();
            }
        }
  
        public void SetIcons()
        {
            Uri iconUri = new Uri("E:/учеба/2 Сем/курсач оооп/featured32@wdd2x.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        
        #region Time
        private void Time()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => {
                txb_Selected.Text = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            };
            timer.Start();
        }
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Time();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenLoginWindow();
        }

        private async void GetAllMessages(object sender, RoutedEventArgs e)
        {
            if (!_proxy.MessageRepositoryProxy.IsAnyUserLoggedIn())
            {
                Configure();
                return;
            }
            MailGrid.ItemsSource = await Task.Run(() => GetAllMessages());
            DeletedGrid.ItemsSource = await Task.Run(() => GetDeletedMessages());
            UnreadGrid.ItemsSource = await Task.Run(() => GetUnseenMessages());
            SentGrid.ItemsSource = await Task.Run(() => GetSentMessages());
            SpamGrid.ItemsSource = await Task.Run(() => GetSpamMessages());
        }

        private List<ShortenMessageModel> GetAllMessages()
        {
            var mail = _proxy.MessageRepositoryProxy.GetAllMessages()
                .OrderByDescending(m => m.Date)
                .ToList();
            messages = _mapper.Map<List<Message>, List<MessageModel>>(mail);
            return _mapper.Map<List<MessageModel>, List<ShortenMessageModel>>(messages);
        }

        private List<ShortenMessageModel> GetDeletedMessages()
        {
            var mail = _proxy.MessageRepositoryProxy.GetDeletedMessages()
                .OrderByDescending(m => m.Date)
                .ToList();
            var messages = _mapper.Map<List<Message>, List<MessageModel>>(mail);
            return _mapper.Map<List<MessageModel>, List<ShortenMessageModel>>(messages);
        }

        private List<ShortenMessageModel> GetSentMessages()
        {
            var mail = _proxy.MessageRepositoryProxy.GetSentMessages()
                .OrderByDescending(m => m.Date)
                .ToList();
            var messages = _mapper.Map<List<Message>, List<MessageModel>>(mail);
            return _mapper.Map<List<MessageModel>, List<ShortenMessageModel>>(messages);
        }

        private List<ShortenMessageModel> GetUnseenMessages()
        {
            var mail = _proxy.MessageRepositoryProxy.GetUnseenMessages()
                .OrderByDescending(m => m.Date)
                .ToList();
            var messages = _mapper.Map<List<Message>, List<MessageModel>>(mail);
            return _mapper.Map<List<MessageModel>, List<ShortenMessageModel>>(messages);
        }

        private List<ShortenMessageModel> GetSpamMessages()
        {
            var mail = _proxy.MessageRepositoryProxy.GetSpamMessages()
                .OrderByDescending(m => m.Date)
                .ToList();
            var messages = _mapper.Map<List<Message>, List<MessageModel>>(mail);
            return _mapper.Map<List<MessageModel>, List<ShortenMessageModel>>(messages);
        }

        private void NewMessageMenuItemClick(object sender, RoutedEventArgs e)
        {
            NewMessage newMessage = new NewMessage();
            newMessage.Owner = this;
            newMessage.Show();
        }

        private void ShowLicenseMenuItemClick(object sender, RoutedEventArgs e)
        {
            LicenseWindow licenseWindow = new LicenseWindow();
            licenseWindow.Owner = this;
            licenseWindow.Show();
        }

        private void NewMessageButtonClick(object sender, RoutedEventArgs e)
        {
            NewMessageMenuItemClick(sender, e);
        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButtonClick(object sender, RoutedEventArgs e)
        {
            GetAllMessages();
        }

        
        private void MailGrid_SelectedCell(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
       
        }
    }
}

