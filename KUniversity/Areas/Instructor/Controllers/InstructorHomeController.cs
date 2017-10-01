using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace KUniversity.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class InstructorHomeController : Controller
    {
        private Models.Instructor _instructor;
        private int adminDeptID = -1;
        private int htEClassID = -1;

        private readonly KUniversityContext _context;

        public InstructorHomeController(KUniversityContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            SetViewData();

            var ins = await _context.Instructor
                .Where(i => i.Id == _instructor.Id)
                .Include(i => i.Courseassignment)
                    .ThenInclude(ca => ca.Course)
                        .ThenInclude(c => c.Department)
                            .ThenInclude(d => d.Campus)
                .AsNoTracking()
                .FirstAsync();

            return View(ins);
        }

        [ActionName("Profile")]
        public async Task<IActionResult> ProfileAsync()
        {
            SetViewData();

            var ins = await _context.Instructor
                .Where(i => i.Id == _instructor.Id)
                .Include(i => i.Courseassignment)
                    .ThenInclude(ca => ca.Course)
                        .ThenInclude(c => c.Department)
                            .ThenInclude(d => d.Campus)
                .AsNoTracking()
                .FirstAsync();

            return View(ins);
        }

        [ActionName("Course")]
        public async Task<IActionResult> CourseAsync()
        {
            SetViewData();

            var ins = await _context.Instructor
                .Where(i => i.Id == _instructor.Id)
                .Include(i => i.Courseassignment)
                    .ThenInclude(ca => ca.Course)
                        .ThenInclude(c => c.Department)
                            .ThenInclude(d => d.Campus)
                .AsNoTracking()
                .FirstAsync();

            return View(ins);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,Name,Gender,Nation,HireDate,PhoneNumber,Email,Academic,OfficeLocation,Password")] Models.Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile");
            }
            return RedirectToAction("Profile");
        }

        [ActionName("CourseScore")]
        public async Task<IActionResult> CourseScoreAsync(int? id)
        {
            SetViewData();

            var ens = await _context.Enrollment
                .Where(e => e.CourseAssignmentId == id)
                .Where(e => e.CourseAssignment.InstructorId == _instructor.Id)
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
                return Content($"{_instructor.Id}");
            }

            return View(ens);
        }

        private void SetViewData()
        {
            SetAccount();
            ViewData["Gender"] = _instructor.Gender;
            ViewData["AccountName"] = _instructor.Name;

            adminDeptID = _context.Department.SingleOrDefault(d => d.InstructorId == _instructor.Id)?.Id ?? -1;

            ViewData["AdminDeptID"] = adminDeptID;

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

        private bool InstructorExists(int id)
        {
            return _context.Instructor.Any(e => e.Id == id);
        }
    }
}
