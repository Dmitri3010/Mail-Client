using System;
using System.ComponentModel.DataAnnotations;

namespace MailClient.DAL.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Hostname { get; set; }

        public string Username { get; set; }
    }
}