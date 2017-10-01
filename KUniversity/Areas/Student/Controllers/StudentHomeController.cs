using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KUniversity.Models;

namespace KUniversity.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentHomeController : Controller
    {
        private Models.Student _student;
        private readonly KUniversityContext _context;

        private void SetAccount()
        {
            if (_student != null) return;

            int id = Program.AccoutnID;
            //int id = HttpContext.Session.Get<int>("Account");
            _student = _context.Student.SingleOrDefault(i => i.Id == id);
            //_student = _context.Students.First();
            ViewData["Gender"] = _student.Gender;
            ViewData["AccountName"] = _student.Name;
        }

        public StudentHomeController(KUniversityContext context)
        {
            _context = context;
        }

        // GET: Student/StudentHome
        public async Task<IActionResult> Index()
        {
            SetAccount();
            var student = await _context.Student
                .Where(i => i.Id == _student.Id)
                .Include(i => i.Enrollment)
                    .ThenInclude(i => i.CourseAssignment)
                        .ThenInclude(ca => ca.Course)
                            .ThenInclude(c => c.Department)
                                .ThenInclude(d => d.Campus)
                .Include(i => i.Enrollment)
                    .ThenInclude(i => i.CourseAssignment)
                        .ThenInclude(ca => ca.Instructor)
                .AsNoTracking()
                .FirstAsync();

            return View(student);
        }
        
        [ActionName("Course")]
        public async Task<IActionResult> CourseAsync()
        {
            SetAccount();
            var student = await _context.Student
                .Where(i => i.Id == _student.Id)
                .Include(i => i.Enrollment)
                    .ThenInclude(i => i.CourseAssignment)
                        .ThenInclude(ca => ca.Course)
                            .ThenInclude(c => c.Department)
                                .ThenInclude(d => d.Campus)
                .Include(i => i.Enrollment)
                    .ThenInclude(i => i.CourseAssignment)
                        .ThenInclude(ca => ca.Instructor)
                .AsNoTracking()
                .FirstAsync();

            return View(student);
        }


        [ActionName("CourseInfo")]
        public async Task<IActionResult> CourseInfoAsync(int? id)
        {
            SetAccount();

            var ens = await _context.Enrollment
                .Where(e => e.CourseAssignmentId == id)
                .Include(e => e.CourseAssignment)
                    .ThenInclude(ca => ca.Course)
                        .ThenInclude(c => c.Department)
                            .ThenInclude(d => d.Campus)
                .Include(e => e.CourseAssignment)
                    .ThenInclude(ca => ca.Instructor)
                .Include(e => e.Student)
                    .ThenInclude(s => s.Eclass)
                        .ThenInclude(ec => ec.Major)
                .AsNoTracking()
                .ToListAsync();

            if (ens == null || ens.Count() < 1)
            {
                return Content($"{_student.Id}");
            }

            return View(ens);
        }

        [ActionName("Score")]
        public async Task<IActionResult> ScoreAsync()
        {
            SetAccount();
            var student = await _context.Student
                .Where(i => i.Id == _student.Id)
                .Include(i => i.Enrollment)
                    .ThenInclude(i => i.CourseAssignment)
                        .ThenInclude(ca => ca.Course)
                .Include(i => i.Enrollment)
                    .ThenInclude(i => i.CourseAssignment)
                        .ThenInclude(ca => ca.Instructor)
                .AsNoTracking()
                .FirstAsync();

            return View(student);
        }

        [ActionName("EClass")]
        public async Task<IActionResult> EClassAsync()
        {
            SetAccount();
            var classes = await _context.Eclass
                .Include(e => e.Instructor)
                .Include(e => e.Major)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(e => e.Id == _student.EclassId);

            return View(classes);
        }

        // GET: Student/StudentHome/Details/5
        public IActionResult Details()
        {
            SetAccount();
            return View(_student);
        }

        // GET: Student/StudentHome/Create
        public IActionResult Create()
        {
            ViewData["EClassID"] = new SelectList(_context.Eclass, "Id", "Id");
            return View();
        }

        // POST: Student/StudentHome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EclassId,Name,Gender,Nation,EnrollmentDate,PhoneNumber,Email,Password")] Models.Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EClassID"] = new SelectList(_context.Eclass, "Id", "Id", student.EclassId);
            return View(student);
        }

        // GET: Student/StudentHome/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["EClassID"] = new SelectList(_context.Eclass, "Id", "Id", student.EclassId);
            return RedirectToAction("Details");
        }

        // POST: Student/StudentHome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EclassId,Name,Gender,Nation,EnrollmentDate,PhoneNumber,Email,Password")] Models.Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details");
            }
            ViewData["EClassID"] = new SelectList(_context.Eclass, "Id", "Id", student.EclassId);
            return RedirectToAction("Details");
        }

        // GET: Student/StudentHome/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Eclass)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/StudentHome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
