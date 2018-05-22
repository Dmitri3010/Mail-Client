using MailClinet.Implementation.Interfaces;
using MailClinet.Implementation.Models;
using System.Collections.Generic;
using System.Linq;

namespace MailClinet.Implementation.Services {
    public abstract class AuthService<TUser, TClient> : IAuthService<TUser, TClient>
        where TUser : IApplicationUser<TClient>
        where TClient : class
    {
        protected List<TUser> users;

        public AuthService() {
            users = new List<TUser>();
        }

        public bool IsAnyUserLoggedIn() {
            return users.Count > 0;
        }

        public void Login(string hostname, string username, string password) {
            TUser user = GetApplicationUserModel(hostname, username, password);
            users.Add(user);
        }

        public void Logout(string username) {
            TUser user = users.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null) {
                users.Remove(user);
            }
        }

        public TUser GetApplicationUserProfile(string username) {
            return users.FirstOrDefault(u => u.Username.Equals(username));
        }

        public List<string> GetUsernamesOfAllUsersLoggedIn() {
            return users.Select(u => u.Username).ToList();
        }

        protected abstract TUser GetApplicationUserModel(string hostname, string username, string password);
    }
}
