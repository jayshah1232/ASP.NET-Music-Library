using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment3_WEB524.EntityModels
{
    public class Track
    {
        public Track()
        {
            Albums = new List<Album>();
        }

        public byte[] Audio { get; set; }
        
        [StringLength(200)]
        public string AudioType { get; set; }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        public string Composers { get; set; }

        [Required]
        public string Genre { get; set; }

        // User name who added/edited the track
        [Required, StringLength(200)]
        public string Clerk { get; set; }

        public ICollection<Album> Albums { get; set; }

        [StringLength(200)]
        public string UploadContentType { get; set; }

        public byte[] Upload { get; set; }
    }
}