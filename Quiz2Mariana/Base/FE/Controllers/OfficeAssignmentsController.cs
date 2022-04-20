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
    public class OfficeAssignmentsController : Controller
    {

        private readonly IOfficeAssignmentsServices officeAssignmentsServices;
        private readonly IPersonServices personServices;

        public OfficeAssignmentsController(IOfficeAssignmentsServices officeAssignmentsServices, IPersonServices personServices)
            {
                this.officeAssignmentsServices = officeAssignmentsServices;
                this.personServices = personServices;
            }

            // GET: OfficeAssignment
            public async Task<IActionResult> Index()
            {
                //var akiraToriyamaContext = _context.Capitulo.Include(c => c.IdAnimeNavigation).Include(c => c.IdTemporadaNavigation);
                return View(await officeAssignmentsServices.GetAllAsync());
            }

            // GET: OfficeAssignment/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var capitulo = await officeAssignmentsServices.GetOneByIdAsync((int)id);
                if (capitulo == null)
                {
                    return NotFound();
                }

                return View(capitulo);
            }

            // GET: OfficeAssignment/Create
            public IActionResult Create()
            {
            ViewData["InstructorId"] = new SelectList(personServices.GetAll(), "Id", "Discriminator"); ;
            return View();
            }

            // POST: OfficeAssignment/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("InstructorId,Location")] OfficeAssignment officeAssignment)
            {
                if (ModelState.IsValid)
                {
                officeAssignmentsServices.Insert(officeAssignment);
                    return RedirectToAction(nameof(Index));
                }
              
                ViewData["InstructorID"] = new SelectList(personServices.GetAll(), "Id", "Discriminator", officeAssignment.InstructorId);
                return View(officeAssignment);
            }

            // GET: OfficeAssignment/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var capitulo = officeAssignmentsServices.GetOneById((int)id);
                if (capitulo == null)
                {
                    return NotFound();
                }
                ViewData["InstructorId"] = new SelectList(officeAssignmentsServices.GetAll(), "Id", "Discriminator", capitulo.InstructorId);
                return View(capitulo);
            }

            // POST: OfficeAssignment/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("InstructorId,Location")] OfficeAssignment officeAssignment)
            {
                if (id != officeAssignment.InstructorId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                       officeAssignmentsServices.Update(officeAssignment);
                    }
                    catch (Exception ee)
                    {
                        if (!OfficeAssignmentExists(officeAssignment.InstructorId))
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
                ViewData["InstructorId"] = new SelectList(officeAssignmentsServices.GetAll(), "Id", "Discriminator", officeAssignment.InstructorId);
                return View(officeAssignment);
            }

            // GET: OfficeAssignment/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var capitulo = officeAssignmentsServices.GetOneByIdAsync((int)id);
                if (capitulo == null)
                {
                    return NotFound();
                }

                return View(capitulo);
            }

            // POST: OfficeAssignment/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var capitulo = officeAssignmentsServices.GetOneById((int)id);
               officeAssignmentsServices.Delete(capitulo);
                return RedirectToAction(nameof(Index));
            }

            private bool OfficeAssignmentExists(int id)
            {
                return (officeAssignmentsServices.GetOneById((int)id)) != null;
            }
        }
    
}
