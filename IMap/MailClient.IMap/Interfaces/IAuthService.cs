using MailClient.IMap.Models;
using System.Collections.Generic;

namespace MailClient.IMap.Interfaces {
    public interface IAuthService {
        bool IsAnyUserLoggedIn();

        void Login(string hostname, string username, string password);

        void Logout(string username);

        ApplicationUser GetApplicationUserProfile(string username);

        List<string> GetUsernamesOfAllUsersLoggedIn();
    }
}
