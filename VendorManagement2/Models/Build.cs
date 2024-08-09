using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendorManagement2.Models
{
    public class Build
    {
        [Key]
        public int BuildID { get; set; }
        public string BuildName { get; set; }
        public string BuildDescription { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public virtual ICollection<BuildComponent> BuildComponents { get; set; }
    }

    public class BuildDto
    {
        public int BuildID { get; set; }
        public string BuildName { get; set; }
        public string BuildDescription { get; set; }
        public List<ComponentDto> Components { get; set; }
    }
}