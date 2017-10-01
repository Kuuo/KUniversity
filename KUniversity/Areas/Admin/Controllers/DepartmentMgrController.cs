using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KUniversity.Models;

namespace KUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentMgrController : Controller
    {
        private readonly KUniversityContext _context;

        public DepartmentMgrController(KUniversityContext context)
        {
            _context = context;    
        }

        // GET: Admin/DepartmentMgr
        public async Task<IActionResult> Index()
        {
            var kUniversityContext = _context.Department.Include(d => d.Instructor).Include(d => d.Campus);
            return View(await kUniversityContext.ToListAsync());
        }

        // GET: Admin/DepartmentMgr/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Instructor)
                .Include(d => d.Campus)
                .Include(d => d.Major)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Admin/DepartmentMgr/Create
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name");
            ViewData["CampusID"] = new SelectList(_context.Campus, "Id", "Name");
            return View();
        }

        // POST: Admin/DepartmentMgr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CampusId,InstructorId,Name,StartTime")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name", department.InstructorId);
            ViewData["CampusID"] = new SelectList(_context.Campus, "Id", "Name", department.CampusId);
            return View(department);
        }

        // GET: Admin/DepartmentMgr/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.SingleOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name", department.InstructorId);
            ViewData["CampusID"] = new SelectList(_context.Campus, "Id", "Name", department.CampusId);
            return View(department);
        }

        // POST: Admin/DepartmentMgr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CampusId,InstructorId,Name,StartTime")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            ViewData["InstructorID"] = new SelectList(_context.Instructor, "Id", "Name", department.InstructorId);
            ViewData["CampusID"] = new SelectList(_context.Campus, "Id", "Name", department.CampusId);
            return View(department);
        }

        // GET: Admin/DepartmentMgr/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Instructor)
                .Include(d => d.Campus)
                .Include(d => d.Major)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Admin/DepartmentMgr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.SingleOrDefaultAsync(m => m.Id == id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
