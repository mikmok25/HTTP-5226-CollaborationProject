using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendorManagement2.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventLocation { get; set; }
        public string EventType { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Foreign key for Category
        public int? CategoryID { get; set; }

        // Navigation property
        public virtual ICollection<CategoryEvent> CategoryEvents { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }

    public class EventDto
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventLocation { get; set; }
        public string EventType { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }
    }
}