using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NR.Core.Repository;
using NR.WebUI.Models;

namespace NR.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ClientesRepository clientesRepository;

        public HomeController(
            ILogger<HomeController> logger, 
            ClientesRepository _clientesRepository
         )
        {
            _logger = logger;
            clientesRepository = _clientesRepository;
        }

        public IActionResult Index()
        {
            //return View(new Driver.Clientes().GetAll());
            return View(clientesRepository.GetAll());
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

        public IActionResult SaveWithInjection(Core.Models.Clientes c)
        {
            var clienteNew = clientesRepository.Add(c);
            return View(clienteNew);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
