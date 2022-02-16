using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guia1MarianaE.Controllers
{
    public class parOimpar : Controller
    {
        public IActionResult par()
        {
            return View();
        }

        public IActionResult impar()
        {
            return View();
        }
    }
}
