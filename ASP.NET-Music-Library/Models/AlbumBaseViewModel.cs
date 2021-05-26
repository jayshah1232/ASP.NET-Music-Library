using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class AlbumBaseViewModel
    {
        public AlbumBaseViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        public DateTime ReleaseDate { get; set; }

        public string Name { get; set; }

        public string UrlAlbum { get; set; }
        
        public int Id { get; set; }

        public string Genre { get; set; }
    }
}