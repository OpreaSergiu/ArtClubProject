using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtClub.Models
{
    public class CostsModels
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public float Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}")]
        public DateTime Month { get; set; }
    }
}