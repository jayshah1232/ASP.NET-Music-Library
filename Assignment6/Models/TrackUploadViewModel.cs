using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class TrackUploadViewModel
    {
        public int Id { get; set; }

        public string AudioType { get; set; }

        public byte[] Audio { get; set; }
    }
}