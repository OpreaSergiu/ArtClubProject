using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtClub.Models
{
    public class ApprovedReservationsModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int LocationReserved { get; set; }
        public string User { get; set; }
        public string Phone { get; set; }
    }
}