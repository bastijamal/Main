using System.ComponentModel.DataAnnotations.Schema;

namespace _17may.Models
{
    public class Doctors
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Position { get; set; }
        public string? Photo { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
