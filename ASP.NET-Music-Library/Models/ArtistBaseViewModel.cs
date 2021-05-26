using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class ArtistBaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "If applicable, artist's birth name")]
        [StringLength(120)]
        public string BirthName { get; set; }

        [Display(Name = "Birth data, or start date")]
        [Required]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Executive who looks after this artist")]
        [Required]
        [StringLength(120)]
        public string Executive { get; set; }

        [Display(Name = "Artist's primary genre")]
        [StringLength(120)]
        public string Genre { get; set; }

        [Display(Name = "Artist Name or Stage Name")]
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        [Display(Name = "Artist Photo")]
        [Required]
        [StringLength(120)]
        public string UrlArtist { get; set; }
    }
}