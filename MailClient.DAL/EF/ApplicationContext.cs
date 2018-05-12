using MailClient.DAL.Entities;
using System.Data.Entity;

namespace MailClient.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=ApplicationContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
