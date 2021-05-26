using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {
        [Display(Name = "Album summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        public IEnumerable<ArtistMediaItemBaseViewModel> ArtistMediaItems { get; set; }
    }
}