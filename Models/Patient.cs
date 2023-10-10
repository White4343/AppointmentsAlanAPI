using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlanAPI.Models
{
    public class Patient
    {
        [Key] 
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        [ForeignKey("User")]
        public long TelegramId { get; set; }
    }
}
