using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS_Management_System.DataContext;
using RMS_Management_System.Models;

namespace RMS_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly DatabaseFile context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StudentController(DatabaseFile context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public  IActionResult StudentRegistration()
        {
            ViewBag.data =  context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentRegistration(StudentViewModel svm)
        {
            if(ModelState.IsValid)
            {
                string FileLocation = UploadFile(svm);

                var student = new Student()
                {
                    StudentName = svm.StudentName,
                    Parentage = svm.Parentage,
                    StudentAge = svm.StudentAge,
                    StudentEmail = svm.StudentEmail,
                    Adhaar = svm.Adhaar,
                    DOB = svm.DOB,
                    Course = svm.Course,
                    StudentPhone = svm.StudentPhone,
                    StudentPhoto = FileLocation,
                };

                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            } 
            return View();
        }




        private string UploadFile(StudentViewModel vm)
        {
            string filename = null;
            if (vm.StudentPhoto != null && vm.StudentPhoto.Length > 0)
            {
                string filedir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "-" + vm.StudentPhoto.FileName;
                string filepath = Path.Combine(filedir, filename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    vm.StudentPhoto.CopyTo(filestream);
                }
            }
            return filename;
        }



        [HttpGet]
        public async Task<IActionResult> GetStudentsList()
        {
            var data = await context.Students.Include(c => c.CourseList).ToListAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> ShowStudents()
        {
            var data = await context.Students.Include(c=>c.CourseList).ToListAsync();
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetSingleStudent(int id)
        {
            /* var data =  await context.Students.Include(c => c.CourseList).Where(x => x.StudentId == id).ToListAsync();*/
            var data = await context.Students.Include(c=>c.CourseList).Where(x=>x.StudentId ==id).ToListAsync();
            return View(data);
        }

    }
}
