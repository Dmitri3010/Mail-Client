namespace MailClinet.Implementation.Models {
    public interface IApplicationUser<TClient> where TClient : class {
        string Hostname { get; set; }

        string Username { get; set; }

        TClient Client { get; set; }
    }
}
