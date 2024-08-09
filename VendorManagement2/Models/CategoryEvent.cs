using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendorManagement2.Models
{
    public class CategoryEvent
    {
        [Key]
        public int CategoryEventID { get; set; }

        // Foreign key for Category
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        // Foreign key for Event
        [ForeignKey("Event")]
        public int EventID { get; set; }
        public virtual Event Event { get; set; }

        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CategoryEventDto
    {
        public int CategoryEventID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
