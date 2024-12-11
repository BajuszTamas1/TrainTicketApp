using System.ComponentModel.DataAnnotations;

namespace TrainTicketApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
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
        public string Status { get; set; } = "Active";
        public DateTime OrderDate { get; set; } = DateTime.Now;  
        [Required]
        public string DayOfWeek { get; set; }
        [Required]
        public TimeSpan DepartureTime { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
    }
}