using System.Collections.Generic;

namespace VendorManagement2.Models.ViewModels
{
    public class EventsAndUsersViewModel
    {
        public IEnumerable<EventDto> Events { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}
