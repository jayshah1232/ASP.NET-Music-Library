using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class AlbumAddViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int GenreId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        public int Id { get; set; }

        [Required]
        public string UrlAlbum { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}