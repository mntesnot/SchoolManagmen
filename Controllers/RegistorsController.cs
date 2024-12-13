using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagmen.Data;
using SchoolManagmen.Models;

namespace SchoolManagmen.Controllers
{
    [Authorize]
    public class RegistorsController : Controller
    {
        private readonly MyDataContext _context;

        public RegistorsController(MyDataContext context)
        {
            _context = context;
        }

        // GET: Registors
        public async Task<IActionResult> Index()
        {
            var myDataContext = _context.Registor.Include(r => r.Course).Include(r => r.Student);
            return View(await myDataContext.ToListAsync());
        }

        // GET: Registors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registor = await _context.Registor
                .Include(r => r.Course)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registor == null)
            {
                return NotFound();
            }

            return View(registor);
        }

        // GET: Registors/Create
        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Name");
            ViewBag.StudentID = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: Registors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registor registor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", registor.CourseId);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Name", registor.StudentID);
            return View(registor);
        }

        // GET: Registors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registor = await _context.Registor.FindAsync(id);
            if (registor == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", registor.CourseId);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Name", registor.StudentID);
            return View(registor);
        }        // To protect from overposting attacks, enable the specific properties you want to bind to.


        // POST: Registors/Edit/5
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Registor registor)
        {
            if (id != registor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistorExists(registor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", registor.CourseId);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Name", registor.StudentID);
            return View(registor);
        }

        // GET: Registors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registor = await _context.Registor
                .Include(r => r.Course)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registor == null)
            {
                return NotFound();
            }

            return View(registor);
        }

        // POST: Registors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registor = await _context.Registor.FindAsync(id);
            if (registor != null)
            {
                _context.Registor.Remove(registor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistorExists(int id)
        {
            return _context.Registor.Any(e => e.Id == id);
        }
    }
}
