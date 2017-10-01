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
    public class HTEClassesController : Controller
    {
        private Models.Instructor _instructor;
        private int adminDeptID = -1;
        private int htEClassID = -1;

        private readonly KUniversityContext _context;

        public HTEClassesController(KUniversityContext context)
        {
            _context = context;    
        }

        // GET: Instructor/HTEClasses
        public async Task<IActionResult> Index()
        {
            SetViewData();
            var classes = await _context.Eclass
                .Include(e => e.Instructor)
                .Include(e => e.Major)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(e => e.Id == htEClassID);

            return View(classes);
        }

        // GET: Instructor/HTEClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            SetViewData();
            if (id == null)
            {
                return NotFound();
            }

            var eClass = await _context.Eclass
                .Include(e => e.Instructor)
                .Include(e => e.Major)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eClass == null)
            {
                return NotFound();
            }

            return View(eClass);
        }

        // GET: Instructor/HTEClasses/Create
        public IActionResult Create()
        {
            SetViewData();
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name");
            ViewData["MajorID"] = new SelectList(_context.Major, "Id", "Title");
            return View();
        }

        // POST: Instructor/HTEClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InstructorID,MajorID,StartYear,Order")] Eclass eClass)
        {
            SetViewData();
            if (ModelState.IsValid)
            {
                _context.Add(eClass);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name", eClass.InstructorId);
            ViewData["MajorID"] = new SelectList(_context.Major, "Id", "Title", eClass.MajorId);
            return View(eClass);
        }

        // GET: Instructor/HTEClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SetViewData();
            if (id == null)
            {
                return NotFound();
            }

            var eClass = await _context.Eclass.SingleOrDefaultAsync(m => m.Id == id);
            if (eClass == null)
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name", eClass.InstructorId);
            ViewData["MajorID"] = new SelectList(_context.Major, "Id", "Title", eClass.MajorId);
            return View(eClass);
        }

        // POST: Instructor/HTEClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InstructorID,MajorID,StartYear,Order")] Eclass eClass)
        {
            SetViewData();
            if (id != eClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EClassExists(eClass.Id))
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
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name", eClass.InstructorId);
            ViewData["MajorID"] = new SelectList(_context.Major, "Id", "Title", eClass.MajorId);
            return View(eClass);
        }

        // GET: Instructor/HTEClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            SetViewData();
            if (id == null)
            {
                return NotFound();
            }

            var eClass = await _context.Eclass
                .Include(e => e.Instructor)
                .Include(e => e.Major)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eClass == null)
            {
                return NotFound();
            }

            return View(eClass);
        }

        // POST: Instructor/HTEClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetViewData();
            var eClass = await _context.Eclass.SingleOrDefaultAsync(m => m.Id == id);
            _context.Eclass.Remove(eClass);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EClassExists(int id)
        {
            return _context.Eclass.Any(e => e.Id == id);
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
