using System.ComponentModel.DataAnnotations;

namespace AlanAPI.Models
{
    public class User
    {
        [Key]
        public long TelegramId { get; set; }
        public string? FullName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? City { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
