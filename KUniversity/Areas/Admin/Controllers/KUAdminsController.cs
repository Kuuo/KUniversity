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
    public class KUAdminsController : Controller
    {
        private readonly KUniversityContext _context;

        public KUAdminsController(KUniversityContext context)
        {
            _context = context;
        }

        // GET: Admin/KUAdmins
        public IActionResult Index()
        {
            var vm = new AdminIndexData()
            {
                Campuses = _context.Campus.Include(c => c.Department),
                Departments = _context.Department.Include(d => d.Major)
            };
            return View(vm);
        }

        // GET: Admin/KUAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kUAdmin = await _context.Admin
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kUAdmin == null)
            {
                return NotFound();
            }

            return View(kUAdmin);
        }

        // GET: Admin/KUAdmins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/KUAdmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Password")] Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/KUAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kUAdmin = await _context.Admin.SingleOrDefaultAsync(m => m.Id == id);
            if (kUAdmin == null)
            {
                return NotFound();
            }
            return View(kUAdmin);
        }

        // POST: Admin/KUAdmins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,Password")] Models.Admin kUAdmin)
        {
            if (id != kUAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kUAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KUAdminExists(kUAdmin.Id))
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
            return View(kUAdmin);
        }

        // GET: Admin/KUAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kUAdmin = await _context.Admin
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kUAdmin == null)
            {
                return NotFound();
            }

            return View(kUAdmin);
        }

        // POST: Admin/KUAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kUAdmin = await _context.Admin.SingleOrDefaultAsync(m => m.Id == id);
            _context.Admin.Remove(kUAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KUAdminExists(int id)
        {
            return _context.Admin.Any(e => e.Id == id);
        }
    }
}
