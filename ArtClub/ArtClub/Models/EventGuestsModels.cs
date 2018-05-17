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
        public string EventName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string GuestEmail { get; set; }
    }
}