using _17may.Models;

namespace _17may.ViewModels
{
    public class HomeVm
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public IFormFile ImgFile { get; set; }
        public List<Doctors> Doctors{ get; set; }


    }
}
