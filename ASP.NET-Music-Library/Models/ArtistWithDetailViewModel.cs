using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {
        [Display(Name = "Artist biography")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        public IEnumerable<ArtistMediaItemBaseViewModel> ArtistMediaItems { get; set; }
    }
}