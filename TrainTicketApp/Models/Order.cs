// TrainTicketApp/Models/Order.cs
using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserAddress { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string TicketType { get; set; }
        public int TrainId { get; set; }
        public Train Train { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}