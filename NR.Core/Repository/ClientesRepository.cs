using Microsoft.AspNetCore.Mvc.Rendering;
using NR.Core.Interfaz;
using NR.Core.Models;
using NR.Core.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NR.Core.Repository
{
    public class ClientesRepository : IClientes
    {

        public ClientesRepository() : base()
        {

        }

        public List<Clientes> GetAll()
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            return clientesDB.GetAll();
        }

        public List<Clientes> GetAll(int Index, int TopReg)
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            return clientesDB.GetAll(Index, TopReg);
        }

        public Clientes Get(int id)
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            return clientesDB.Get(id);
        }

        public ResultView Add(Clientes cliente)
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            return clientesDB.Add(cliente);
        }

        public ResultView Update(Clientes cliente)
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            return clientesDB.Update(cliente);
        }

        public ResultView Delete(int id)
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            return clientesDB.Delete(id);
        }

        public List<Clientes> Filter
        (
            string Identificacion,
            string Razon_Social,
            DateTime datetime_createdBetweenFrom,
            DateTime datetime_createdBetweenTo,
            DateTime datetime_createdEqual,
            DateTime datetime_createdMayor,
            DateTime datetime_createdMenor,
            string opeFecha
        )
        {
           Data.ClientesDB clientesDB = new Data.ClientesDB();
           var ListaClientes = clientesDB.Filter(Identificacion, Razon_Social,
                datetime_createdBetweenFrom, datetime_createdBetweenTo, datetime_createdEqual,
                datetime_createdMayor, datetime_createdMenor, opeFecha);

            return ListaClientes;
        }

        public IEnumerable<SelectListItem> ListaSelect()
        {
            NR.Core.Data.ClientesDB clientesDB = new Data.ClientesDB();
            List<Clientes> ListaClientes = clientesDB.GetAll();
            return ListaClientes.ToList().Select(x => new SelectListItem()
            {
                Text   = x.razon_social,
                Value = x.cliente_id.ToString()
            });
        }
    }
}
