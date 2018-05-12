using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailClient.DAL.Entities
{
    public class Message
    {
        public Message()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        [Column(TypeName = "text")]
        public string Body { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}