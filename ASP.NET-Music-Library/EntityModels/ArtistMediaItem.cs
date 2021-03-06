using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.EntityModels
{
    public class ArtistMediaItem
    {
        public ArtistMediaItem()
        {
            TimeStamp = DateTime.Now;
        }
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string Caption { get; set; }

        public string StringId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}