using FE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FE.Models;

namespace FE.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentsServices enrollmentsServices;
        private readonly IPersonServices personServices;
        private readonly ICourseServices courseServices;

        public EnrollmentsController(IEnrollmentsServices enrollmentsServicess, IPersonServices personServices,ICourseServices courseServices)
        {
            this.enrollmentsServices = enrollmentsServicess;
            this.personServices = personServices;
            this.courseServices = courseServices;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            //var akiraToriyamaContext = _context.Capitulo.Include(c => c.IdAnimeNavigation).Include(c => c.IdTemporadaNavigation);
            return View(await enrollmentsServices.GetAllAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capitulo = await enrollmentsServices.GetOneByIdAsync((int)id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(courseServices.GetAll(), "CourseId", "CourseId");
            ViewData["StudentId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,CourseId,StudentId,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollmentsServices.Insert(enrollment);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(courseServices.GetAll(), "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = enrollmentsServices.GetOneById((int)id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(courseServices.GetAll(), "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,CourseId,StudentId,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    enrollmentsServices.Update(enrollment);
                }
                catch (Exception ee)
                {
                    if (!EnrollmentsServicesExists(enrollment.EnrollmentId))
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
            ViewData["CourseId"] = new SelectList(courseServices.GetAll(), "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capitulo = enrollmentsServices.GetOneByIdAsync((int)id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var capitulo = enrollmentsServices.GetOneById((int)id);
            enrollmentsServices.Delete(capitulo);
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentsServicesExists(int id)
        {
            return (enrollmentsServices.GetOneById((int)id)) != null;
        }
    }
}
