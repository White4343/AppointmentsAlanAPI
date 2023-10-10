using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlanAPI.Models
{
    public class TelegramMessage
    {
        [Key]
        public int MessageId { get; set; }
        public required string Content { get; set; }
        [ForeignKey("User")]
        public long TelegramId { get; set; }
        public User User { get; set; }
    }
}
