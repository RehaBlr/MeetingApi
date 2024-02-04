using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Domain.Entity
{
    [Table("Meeting", Schema = "APP")]
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(500)]
        [Column(Order = 1)]
        public string MeetingName { get; set; }
        [Required]
        [Column(Order = 2)]
        public DateTime StartDate { get; set; }
        [Required]
        [Column(Order = 3)]
        public DateTime EndDate { get; set; }
        [Column(Order = 4)]
        public string Description { get; set; }
        [Column(Order = 5)]
        public string? Document { get; set; }

        public bool RecordStatus { get; set; }

        
    }
}
