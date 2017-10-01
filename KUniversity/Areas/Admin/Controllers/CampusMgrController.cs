using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KUniversity.Models;
using KUniversity.Models.ViewModels;

namespace KUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CampusMgrController : Controller
    {
        private readonly KUniversityContext _context;

        public CampusMgrController(KUniversityContext context)
        {
            _context = context;
        }

        // GET: Admin/CampusMgr
        public IActionResult Index()
        {
            var vm = new AdminIndexData()
            {
                Campuses = _context.Campus.Include(c => c.Department),
                Departments = _context.Department.Include(d => d.Major)
            };
            return View(vm);
        }

        // GET: Admin/CampusMgr/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            var data = new CampusDetailsData
            {
                Campus = campus,
                Departments = _context.Department.Where(d => d.CampusId == campus.Id).Include(d => d.Instructor)
            };

            return View(data);
        }

        // GET: Admin/CampusMgr/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CampusMgr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Campus campus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(campus);
        }

        // GET: Admin/CampusMgr/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus.SingleOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }
            return View(campus);
        }

        // POST: Admin/CampusMgr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Campus campus)
        {
            if (id != campus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusExists(campus.Id))
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
            return View(campus);
        }

        // GET: Admin/CampusMgr/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus.Include(c => c.Department)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // POST: Admin/CampusMgr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campus = await _context.Campus.SingleOrDefaultAsync(m => m.Id == id);
            _context.Campus.Remove(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CampusExists(int id)
        {
            return _context.Campus.Any(e => e.Id == id);
        }
    }
}
