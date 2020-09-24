using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NR.WebUI.Models;

namespace NR.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new Driver.Clientes().GetAll());
        }

        public IActionResult Save(Core.Models.Clientes c)
        {
            Driver.Clientes clientes = new Driver.Clientes();
            var clienteNew = new Core.Models.Clientes()
            {
                razon_social = c.razon_social,
                fecha_reg    = DateTime.Today
            };

            clienteNew = clientes.Add(clienteNew);

            return View(clienteNew);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
