using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KUniversity.Models;

namespace KUniversity.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class DepartmentCoursesController : Controller
    {
        private Models.Instructor _instructor;
        private int adminDeptID = -1;
        private int htEClassID = -1;

        private readonly KUniversityContext _context;

        public DepartmentCoursesController(KUniversityContext context)
        {
            _context = context;    
        }

        // GET: Instructor/DepartmentCourses
        public async Task<IActionResult> Index()
        {
            SetViewData();
            var courses = _context.Course
                .Where(c => c.DepartmentId == adminDeptID)
                .Include(c => c.Department);

            return View(await courses.ToListAsync());
        }

        // GET: Instructor/DepartmentCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetViewData();
            var course = await _context.Course
                .Include(c => c.Department)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Instructor/DepartmentCourses/Create
        public IActionResult Create()
        {
            SetViewData();
            ViewData["DepartmentID"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: Instructor/DepartmentCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,Title")] Course course)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepartmentID"] = new SelectList(_context.Department, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Instructor/DepartmentCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetViewData();
            var course = await _context.Course.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Department, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // POST: Instructor/DepartmentCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,Title")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            SetViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["DepartmentID"] = new SelectList(_context.Department, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Instructor/DepartmentCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetViewData();
            var course = await _context.Course
                .Include(c => c.Department)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Instructor/DepartmentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetViewData();
            var course = await _context.Course.SingleOrDefaultAsync(m => m.Id == id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        private void SetViewData()
        {
            SetAccount();
            ViewData["Gender"] = _instructor.Gender;
            ViewData["AccountName"] = _instructor.Name;

            adminDeptID = _context.Department.SingleOrDefault(d => d.InstructorId == _instructor.Id)?.Id ?? -1;

            ViewData["AdminDeptID"] = adminDeptID;

            if (adminDeptID != -1)
            {
                ViewData["Dept"] = _context.Department.SingleOrDefault(d => d.Id == adminDeptID);
            }

            htEClassID = _context.Eclass.FirstOrDefault(e => e.InstructorId == _instructor.Id)?.Id ?? -1;

            ViewData["HTEClassID"] = htEClassID;
        }

        private void SetAccount()
        {
            int id = Program.AccoutnID;
            //int id = HttpContext.Session.Get<int>("Account");
            _instructor = _context.Instructor.SingleOrDefault(i => i.Id == id);
            adminDeptID = -1;
            htEClassID = -1;
        }
    }
}
