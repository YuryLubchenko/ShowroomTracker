using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModel.Entities;

namespace Database.Entities
{
    [Table("EmailSubscribers")]
    internal class EmailSubscriber: IEmailSubscriber
    {
        public EmailSubscriber()
        {
        }

        public EmailSubscriber(IEmailSubscriber subscriber)
        {
            Id = subscriber.Id;
            EMail = subscriber.EMail;
            Disabled = subscriber.Disabled;
        }

        [Key]
        public int Id { get; set; }

        public string EMail { get; set; }

        public bool Disabled { get; set; }
    }
}