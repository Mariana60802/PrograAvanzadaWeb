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
    public class CourseController : Controller
    {
        private readonly IDepartmentsServices departmentsServices;
        private readonly ICourseServices courseServices;

        public CourseController(IDepartmentsServices departmentsServices, ICourseServices courseServices)
        {
            this.departmentsServices = departmentsServices;
            this.courseServices = courseServices;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
       
            return View(await courseServices.GetAllAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capitulo = await courseServices.GetOneByIdAsync((int)id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(courseServices.GetAll(), "DepartmentId", "DepartmentId");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Title,Credits,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                courseServices.Insert(course);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(courseServices.GetAll(), "DepartmentId", "DepartmentId", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = courseServices.GetOneById((int)id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(courseServices.GetAll(), "DepartmentId", "DepartmentId", course.DepartmentId);

            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Credits,DepartmentId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    courseServices.Update(course);
                }
                catch (Exception ee)
                {
                    if (!CourseServicesExists(course.CourseId))
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
            ViewData["DepartmentId"] = new SelectList(courseServices.GetAll(), "DepartmentId", "DepartmentId", course.DepartmentId);
            return View(courseServices);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capitulo = courseServices.GetOneByIdAsync((int)id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var capitulo = courseServices.GetOneById((int)id);
            courseServices.Delete(capitulo);
            return RedirectToAction(nameof(Index));
        }

        private bool CourseServicesExists(int id)
        {
            return (courseServices.GetOneById((int)id)) != null;
        }
    }
}
