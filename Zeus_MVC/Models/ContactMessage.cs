using System;
using System.ComponentModel.DataAnnotations;

namespace Zeus_MVC.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Please enter subject.")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "Please enter your phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public MessageStatus Status { get; set; }
    }

    public enum MessageStatus
    {
        UnRead = 0,
        Read = 1
    }
}