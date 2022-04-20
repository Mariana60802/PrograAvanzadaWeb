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
    public class PeopleController : Controller
    {
        private readonly IDepartmentsServices departmentsServices;
        private readonly IOfficeAssignmentsServices OfficeAssigmentServices;
        private readonly IPersonServices peopleServices;

        public PeopleController(IDepartmentsServices departmentsServices, IOfficeAssignmentsServices OfficeAssigmentServices, IPersonServices peopleServices)
        {
            this.departmentsServices = departmentsServices;
            this.OfficeAssigmentServices = OfficeAssigmentServices;
            this.peopleServices = peopleServices;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {

            return View(await peopleServices.GetAllAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capitulo = await peopleServices.GetOneByIdAsync((int)id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        // GET: People/Create
        public IActionResult Create()
        {
          return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(("Id,LastName,FirstName,HireDate,EnrollmentDate,Discriminator"))] Person person)
        {
            if (ModelState.IsValid)
            {
                peopleServices.Insert(person);
                return RedirectToAction(nameof(Index));
            }

           
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = peopleServices.GetOneById((int)id);
            if (person == null)
            {
                return NotFound();
            }
          
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,HireDate,EnrollmentDate,Discriminator")] Person person)
        {
            
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    peopleServices.Update(person);
                }
                catch (Exception ee)
                {
                    if (!PersonServicesExists(person.Id))
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
           return View(person);
        }

        // GET: People/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capitulo = peopleServices.GetOneByIdAsync((int)id);
            if (capitulo == null)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var capitulo = peopleServices.GetOneById((int)id);
            peopleServices.Delete(capitulo);
            return RedirectToAction(nameof(Index));
        }

        private bool PersonServicesExists(int id)
        {
            return (peopleServices.GetOneById((int)id)) != null;
        }
    }
}



