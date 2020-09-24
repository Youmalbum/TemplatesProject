using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NR.Core.Data;
using NR.Core.Models;
using NR.Core.ModelsView;

namespace NR.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly string _strConexion;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger      = logger;
            _strConexion = Environment.GetEnvironmentVariable("CON_BD_STR");
        }

        [HttpGet]
        public List<Clientes> GetAll()
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            List<Clientes> ListaClientes = clientesDB.GetAll();
            return ListaClientes;
        }

        [HttpGet]
        public List<Clientes> GetAll(int Index, int TopReg)
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            List<Clientes> ListaClientes = clientesDB.GetAll(Index, TopReg);
            return ListaClientes;
        }

        [HttpGet]
        public Clientes Get(int id)
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            Clientes cliente = clientesDB.Get(id);
            return cliente;
        }

        [HttpPost]
        public Clientes Add(Clientes cliente)
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            ResultView resultView = clientesDB.Add(cliente);
            if (resultView.isOk)
                cliente.cliente_id = resultView.LastId;
            else {
                _logger.LogError("Add Clientes", resultView.ErrorMessage);
                return null;
            }

            return cliente;
        }

        [HttpPost]
        public Clientes Update(Clientes cliente)
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            ResultView resultView = clientesDB.Update(cliente);
            if (resultView.isOk)
                return cliente;
            else
            {
                _logger.LogError("Update Clientes", resultView.ErrorMessage);
                return null;
            }
        }

        [HttpPost]
        public bool Delete(int id)
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            ResultView resultView = clientesDB.Delete(id);
            if (resultView.isOk)
                return true;
            else
            {  
                _logger.LogError("Delete Clientes", resultView.ErrorMessage);
                return false;
            }
        }

        [HttpGet]
        public List<Clientes> Filter
        ( 
          string Identificacion, string Razon_Social,
          DateTime datetime_createdBetweenFrom, DateTime datetime_createdBetweenTo,
          DateTime datetime_createdEqual, DateTime datetime_createdMayor,
          DateTime datetime_createdMenor, string opeFecha
        )
        {
            ClientesDB clientesDB = new ClientesDB(_strConexion);
            List<Clientes> ListaClientes = clientesDB.Filtrar(Identificacion, Razon_Social,
                datetime_createdBetweenFrom, datetime_createdBetweenTo, datetime_createdEqual,
                datetime_createdMayor, datetime_createdMenor, opeFecha);

            return ListaClientes;
        }
    }
}
