using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagmen.Data;
using SchoolManagmen.Models;
using SchoolManagmen.ViewModels;

namespace SchoolManagmen.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly MyDataContext _db;
        public StudentsController(MyDataContext db)
        {
            _db = db;
        }
        public IActionResult Index(string name = null)
        {
            if (name == null)
            {
                var data = _db.Students.ToList();
                return View(data);
            }
            else
            {
                var data = _db.Students.Where(a => a.Name.StartsWith(name));
                return View(data);
            }
        }

        public IActionResult StudentGradeReport(int id = -1)
        {
            ViewBag.StudentList= new SelectList(_db.Students, "Id", "Name");

            var model = new GradeReport();
            if(id == -1)
            {
                return View(model);
            }
            else
            {
                model.Student = _db.Students.Find(id);
                model.Registors = _db.Registor.Include(r => r.Course).Where(a=>a.StudentID == id).ToList();
                return View(model);
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Students.Add(model);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                 
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = _db.Students.Find(id);
            if(data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Student updatedStudent)
        {
            if (ModelState.IsValid)
            {
                var data = _db.Students.Find(updatedStudent.Id);
                if (data != null)
                {
                    if (data.Name != updatedStudent.Name)
                        data.Name = updatedStudent.Name;

                    if (data.Age != updatedStudent.Age)
                        data.Age = updatedStudent.Age;

                    if (data.PhoneNo != updatedStudent.PhoneNo)
                        data.PhoneNo = updatedStudent.PhoneNo;

                    _db.Students.Update(data);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(updatedStudent);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _db.Students.Find(id);
            if(data != null)
            {
                _db.Students.Remove(data);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Search(string name)
        {
            var data = _db.Students.Where(a => a.Name.StartsWith(name));
            return View("Index",data);
        }
        public IActionResult Detail(int id)
        {
            var data= _db.Students.Find(id);
            return View(data);
        }
    }
}
