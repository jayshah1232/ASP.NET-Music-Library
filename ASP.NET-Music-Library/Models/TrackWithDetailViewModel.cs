using Assignment3_WEB524.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {

        public int AlbumId { get; set; }
        
        public IEnumerable<Album> Albums { get; set; }

        public IEnumerable<string> AlbumNames { get; set; }

        public string AlbumName { get; set; }

        [Display(Name = "Sample clip")]
        public string AudioURL
        {
            get
            {
                return $"/audio/{Id}";
            }
        }

    }
}