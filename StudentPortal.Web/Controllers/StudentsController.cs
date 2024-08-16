using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        /*private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
        public ApplicationDbContext DbContext { get; } 

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddStudentViewModel viewModel)
        {
            return View(viewModel);
        }*/

        private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = dbContext.students.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        /*[HttpPost]
           public IActionResult AddStudent()
           {
               var name = Request.Form["name"];
               var email = Request.Form["email"];
               var phone = Request.Form["phone"];
               var subscribed = Request.Form["subscribed"].ToString().ToLower() == "true";

               if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
               {
                   // If the form data is not valid, redisplay the form with validation messages
                   ViewData["Error"] = "All fields are required.";
                   return View();
               }

               var student = new Student
               {
                   Name = name,
                   Email = email,
                   Phone = phone,
                   Subscribed = subscribed
               };

               dbContext.students.Add(student);
               dbContext.SaveChanges();

               return RedirectToAction("Index");
           } */

        [HttpPost]
        public IActionResult AddStudent()
        {
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var phone = Request.Form["phone"];
            //var subscribed = Request.Form["subscribed"].ToString().ToLower() == "true";
            var subscribed = Request.Form["subscribed"].ToString().ToLower() == "on";
            //var subscribed = Request.Form["subscribed"] == "on" ? true : false;


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                // If the form data is not valid, redisplay the form with validation messages
                ViewData["Error"] = "All fields are required.";
                return View("Add");
            }

            var student = new Student
            {
                Name = name,
                Email = email,
                Phone = phone,
                Subscribed = subscribed
            };

            dbContext.students.Add(student);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = dbContext.students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                dbContext.students.Update(student);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = dbContext.students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.students.Remove(student);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
