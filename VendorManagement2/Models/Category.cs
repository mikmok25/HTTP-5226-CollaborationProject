using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VendorManagement2.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        // Navigation property to relate Category with Event
        public virtual ICollection<Event> CategoryEvents { get; set; }
    }

    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
