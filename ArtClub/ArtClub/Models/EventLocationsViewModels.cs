using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ArtClub.Models
{
    public class EventLocationsViewModels
    {
        public IEnumerable<LocationsModels> Locations { get; set; }
        public EventsModels Event { get; set; }
    }
}