using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorManagement2.Models.ViewModels
{
    public class DetailsUser
    {
        public UserDto SelectedUser { get; set; }
        public IEnumerable<EventDto> UserEvents { get; set; }
    }
}
