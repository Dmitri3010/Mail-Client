using MailClient.IMap.Models;
using System.Collections.Generic;
using System.Linq;
using S22.Imap;

namespace MailClient.IMap.Services
{
    public class AuthService
    {
        protected List<ApplicationUser> users;

        public AuthService()
        {
            users = new List<ApplicationUser>();
        }

        public bool IsAnyUserLoggedIn()
        {
            return users.Count > 0;
        }

        public void AddApplicationUserProfile(string hostname, string username, string password)
        {
            ApplicationUser user = GetApplicationUserModel(hostname, username, password);
            users.Add(user);
        }

        public void RemoveApplicationUserProfile(string username)
        {
            ApplicationUser user = users.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null)
            {
                users.Remove(user);
            }
        }

        public ApplicationUser GetApplicationUserProfile(string username)
        {
            return users.FirstOrDefault(u => u.Username.Equals(username));
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
