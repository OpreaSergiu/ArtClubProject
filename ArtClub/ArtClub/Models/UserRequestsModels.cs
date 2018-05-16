using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtClub.Models
{
    public class UserRequestsModels
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Intrest { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
    }
}