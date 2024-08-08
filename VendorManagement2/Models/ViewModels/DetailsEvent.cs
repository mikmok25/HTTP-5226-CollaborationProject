using System.Collections.Generic;
using VendorManagement2.Models;

namespace VendorManagement2.Models.ViewModels
{
    public class DetailsEvent
    {
        public DetailsEvent()
        {
            AssociatedUsers = new List<UserDto>(); // Initialize the list to avoid null reference
        }
        public EventDto SelectedEvent { get; set; }

        public IEnumerable<UserDto> AssociatedUsers { get; set; }
        public IEnumerable<UserDto> AvailableUsers { get; set; }
    }
}