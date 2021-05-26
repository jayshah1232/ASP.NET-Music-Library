using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3_WEB524.Models
{
    public class TrackEditFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Clip")]
        [DataType(DataType.Upload)]
        public string TrackUpload { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}