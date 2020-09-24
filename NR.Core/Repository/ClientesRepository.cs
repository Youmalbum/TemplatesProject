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
