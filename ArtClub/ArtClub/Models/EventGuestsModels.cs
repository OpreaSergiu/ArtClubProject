using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtClub.Models
{
    public class EventGuestsModels
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
    }
}