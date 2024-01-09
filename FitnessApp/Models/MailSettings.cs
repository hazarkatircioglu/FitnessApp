using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class MailSettings
    {
        [Key]
        public int MailSettingsId { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromEmailAddressDisplayName { get; set; }
        public string SendEmailAddress { get; set; }
        public string SendEmailAddressDisplayName { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string EmailAddress { get; set; }
        public string EmailAddressPassword { get; set; }
    }
}
