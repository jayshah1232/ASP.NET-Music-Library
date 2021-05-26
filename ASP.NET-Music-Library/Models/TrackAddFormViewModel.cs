using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Models
{
    public class TrackAddFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Track Name")]
        [StringLength(200)]
        public string Name { get; set; }

        public string AlbumName { get; set; }

        public string GenreName { get; set; }

        [Display(Name = "Track Genre")]
        public SelectList GenreList { get; set; }

        public int GenreId { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Composer Names (Comma seperated)")]
        public string Composers { get; set; }

        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }
    }
}