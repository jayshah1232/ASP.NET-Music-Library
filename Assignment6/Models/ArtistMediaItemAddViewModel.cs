using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class ArtistMediaItemAddViewModel
    {
        public int ArtistId { get; set; }

        [Display(Name = "Descriptive caption")]
        [Required]
        [StringLength(100)]
        public string Caption { get; set; }

        public HttpPostedFileBase Upload { get; set; }
    }
}