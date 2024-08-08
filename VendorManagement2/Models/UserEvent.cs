using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendorManagement2.Models
{
    public class UserEvent
    {
        [Key]
        public int UserEventID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Event")]
        public int EventID { get; set; }
        public virtual Event Event { get; set; }

        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserEventDto
    {
        public int UserEventID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}