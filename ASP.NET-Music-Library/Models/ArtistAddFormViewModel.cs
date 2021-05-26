using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Models
{
    public class ArtistAddFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date, or start date")]
        [Required]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }

        [Required]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Artist biography")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
    }
}