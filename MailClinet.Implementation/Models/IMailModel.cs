namespace MailClinet.Implementation.Models {
    public interface IMailModel {
        string From { get; set; }

        string To { get; set; }

        string Subject { get; set; }

        string Body { get; set; }
    }
}
