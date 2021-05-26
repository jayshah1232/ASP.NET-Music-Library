using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class ArtistMediaItemAddFormViewModel
    {
        public int ArtistId { get; set; }

        [Display(Name = "Descriptive caption")]
        [Required, StringLength(100)]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Media item")]
        [DataType(DataType.Upload)]
        public string Upload { get; set; }
    }
}