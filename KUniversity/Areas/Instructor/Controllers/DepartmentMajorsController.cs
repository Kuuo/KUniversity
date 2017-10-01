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
    public class DepartmentMajorsController : Controller
    {
        private Models.Instructor _instructor;
        private int adminDeptID = -1;
        private int htEClassID = -1;

        private readonly KUniversityContext _context;

        public DepartmentMajorsController(KUniversityContext context)
        {
            _context = context;    
        }

        // GET: Instructor/DepartmentMajors
        public async Task<IActionResult> Index()
        {
            SetViewData();
            var majors = await _context.Major
                .Where(m => m.DepartmentId == adminDeptID)
                .Include(m => m.Department)
                .Include(m => m.Eclass)
                .ToListAsync();

            return View(majors);
        }

        // GET: Instructor/DepartmentMajors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            SetViewData();
            if (id == null)
            {
                return NotFound();
            }

            var major = await _context.Major
                .Include(m => m.Department)
                .Include(m => m.Eclass)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // GET: Instructor/DepartmentMajors/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Instructor/DepartmentMajors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,Title")] Major major)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                _context.Add(major);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepartmentID"] = new SelectList(_context.Department, "Id", "Name", major.DepartmentId);
            return View(major);
        }

        // GET: Instructor/DepartmentMajors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SetViewData();
            if (id == null)
            {
                return NotFound();
            }

            var major = await _context.Major.SingleOrDefaultAsync(m => m.Id == id);
            if (major == null)
            {
                return NotFound();
            }
            return View(major);
        }

        // POST: Instructor/DepartmentMajors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,Title")] Major major)
        {
            if (id != major.Id)
            {
                return NotFound();
            }

            SetViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(major);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MajorExists(major.Id))
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
            return View(major);
        }

        // GET: Instructor/DepartmentMajors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetViewData();
            var major = await _context.Major
                .Include(m => m.Department)
                .Include(m => m.Eclass)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // POST: Instructor/DepartmentMajors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetViewData();
            var major = await _context.Major.SingleOrDefaultAsync(m => m.Id == id);
            _context.Major.Remove(major);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MajorExists(int id)
        {
            return _context.Major.Any(e => e.Id == id);
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
            else
            {
                ViewData["Dept"] = _context.Department.SingleOrDefault(d => d.Id == _instructor.DepartmentId);
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
