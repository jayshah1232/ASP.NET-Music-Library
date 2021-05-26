using System.ComponentModel.DataAnnotations;

namespace Assignment3_WEB524.EntityModels
{
    public class Genre
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}