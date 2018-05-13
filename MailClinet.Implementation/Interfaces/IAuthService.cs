using MailClinet.Implementation.Models;
using System.Collections.Generic;

namespace MailClinet.Implementation.Interfaces {
    public interface IAuthService<TUser, TClient> 
        where TUser : IApplicationUser<TClient>
        where TClient : class
    {
        bool IsAnyUserLoggedIn();

        void Login(string hostname, string username, string password);

        void Logout(string username);

        TUser GetApplicationUserProfile(string username);

        List<string> GetUsernamesOfAllUsersLoggedIn();
    }
}
