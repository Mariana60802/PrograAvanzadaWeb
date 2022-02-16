using Guia1MarianaE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guia1MarianaE.Controllers
{
    public class NumerosController : Controller
    {
        // GET: NumerosController
        public ActionResult Index()
        {
            List<SelectListItem> Numeros = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "3" },
                new SelectListItem { Value = "3", Text = "2" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" }
            };

            //assigning SelectListItem to view Bag
            ViewBag.Numeros = Numeros;
            return View();
        }

        // POST: NumerosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Consulta));
            }
            catch
            {
                return View();
            }
        }




        // GET: NumerosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NumerosController/Create
        public ActionResult Create()
        {
            return View();
        }

       
   
    
        public ActionResult Consulta(int id)
        {
            Logica Modelo = new Logica();
            parOimpar num = new parOimpar();

            if (id != null)
            {
                if (Modelo.paroimpar(id) == true)
                {
                   RedirectToAction(nameof(num.par));
                }
                else
                {
                    RedirectToAction(nameof(num.impar));
                }
            }
            else
            {

            }
            return View();
        }

        // POST: NumerosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
