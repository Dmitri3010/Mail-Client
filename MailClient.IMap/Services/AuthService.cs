using MailClient.IMap.Models;
using System.Collections.Generic;
using System.Linq;
using S22.Imap;
using MailClient.IMap.Interfaces;

namespace MailClient.IMap.Services
{
    public class AuthService : IAuthService {
        protected List<ApplicationUser> users;

        public AuthService() {
            users = new List<ApplicationUser>();
        }

        public bool IsAnyUserLoggedIn() {
            return users.Count > 0;
        }

        public void Login(string hostname, string username, string password) {
            ApplicationUser user = GetApplicationUserModel(hostname, username, password);
            users.Add(user);
        }

        public void Logout(string username) {
            ApplicationUser user = users.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null) {
                users.Remove(user);
            }
        }

        public ApplicationUser GetApplicationUserProfile(string username)
        {
            return users.FirstOrDefault(u => u.Username.Equals(username));
        }

        public List<string> GetUsernamesOfAllUsersLoggedIn() 
        {
            return users.Select(u => u.Username).ToList();
        }

        private ApplicationUser GetApplicationUserModel(string hostname, string username, string password)
        {
            return new ApplicationUser
            {
                Hostname = hostname,
                Username = username,
                ImapClient = GetImapClient(hostname, username, password)
            };
        }

        private ImapClient GetImapClient(string hostname, string username, string password)
        {
            return new ImapClient(hostname, 993, username, password, AuthMethod.Login, true);
        }
    }
}
