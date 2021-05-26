using Assignment3_WEB524.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class ArtistAddViewModel
    {
        public int Id { get; set; }

        public string BirthName { get; set; }

        [Required]
        public DateTime BirthOrStartDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UrlArtist { get; set; }

        public int GenreId { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
    }
}