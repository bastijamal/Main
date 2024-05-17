using _17may.DAL;
using _17may.Models;
using _17may.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace _17may.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DoctorsController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Doctors.ToListAsync();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctors homevm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!homevm.ImgFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("ImgFile", "Error!");
                return View();

            }
            string path = _environment.WebRootPath + @"\Upload\Doctors\";
            string filename = Guid.NewGuid() + homevm.ImgFile.FileName;

            
            using (FileStream filestream=new FileStream(path+filename,FileMode.Create))
            {
                homevm.ImgFile.CopyTo(filestream);
            }
      
 
          

            Doctors doctors = new Doctors()
            {
                FullName = homevm.FullName,
                Position = homevm.Position,
                Photo = filename,

            };

            _context.Doctors.Add(doctors);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Remove(int id)
        {
            var doctors = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if (doctors != null)
            {
                _context.Doctors.Remove(doctors);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var slider = _context.Doctors.FirstOrDefault(X => X.Id == id);
            if (slider == null)
            {
                return RedirectToAction("Index");
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Doctors doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var oldDoctor = _context.Doctors.FirstOrDefault(x => x.Id == doctor.Id);
            if (oldDoctor == null) { return RedirectToAction("Index"); }
            oldDoctor.FullName = doctor.FullName;
            oldDoctor.Position = doctor.Position;
            oldDoctor.ImgFile = doctor.ImgFile;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}


 