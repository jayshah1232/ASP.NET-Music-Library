using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class TrackBaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Track name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-seperated)")]
        [Required]
        [StringLength(300)]
        public string Composers { get; set; }
    }
}