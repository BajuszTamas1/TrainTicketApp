// TrainTicketApp/Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}