using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Models
{
    public class AlbumAddFormViewModel
    {
        [Display(Name = "Album name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string ArtistName { get; set; }

        [Display(Name = "Album's primary genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "Album summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        public int Id { get; set; }

        [Display(Name = "URL to album image (cover art)")]
        [Required]
        public string UrlAlbum { get; set; }

        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }
    }
}