using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class TrackAddViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }

        [Required]
        public string Composers { get; set; }

        public int GenreId { get; set; }

        public string Genre { get; set; }
    }
}