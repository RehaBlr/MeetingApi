using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Domain.Entity
{
    [Table("UserMeeting", Schema = "APP")]
    public class UserMeeting
    {
        [Key]
        public int Id { get; set; }    
        public int UserId {  get; set; }
        public User User { get; set;}
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set;}

    }
}
