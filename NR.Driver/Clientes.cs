using NR.Core.Data;
using System;
using System.Collections.Generic;

namespace NR.Driver
{
    public class Clientes : DefaultConnectionStringDataBase
    {
        public List<Core.Models.Clientes> GetAll()
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            List<Core.Models.Clientes> ListaClientes = clientesDB.GetAll();
            return ListaClientes;
        }

        public List<Core.Models.Clientes> GetAll(int Index, int TopReg)
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            List<Core.Models.Clientes> ListaClientes = clientesDB.GetAll(Index, TopReg);
            return ListaClientes;
        }

        public Core.Models.Clientes Get(int id)
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            Core.Models.Clientes cliente = clientesDB.Get(id);
            return cliente;
        }

        public Core.Models.Clientes Add(Core.Models.Clientes cliente)
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            Core.ModelsView.ResultView resultView = clientesDB.Add(cliente);
            if (resultView.isOk)
            {
                cliente.cliente_id = resultView.LastId;
            }
            else
            {
                throw new Exception("ERROR: " + resultView.ErrorMessage);
            }

            return cliente;
        }

        public Core.Models.Clientes Update(Core.Models.Clientes cliente)
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            Core.ModelsView.ResultView resultView = clientesDB.Update(cliente);
            if (!resultView.isOk)
            {
                throw new Exception("ERROR: " + resultView.ErrorMessage);
            }

            return cliente;
        }

        public bool Delete(int id)
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            Core.ModelsView.ResultView resultView = clientesDB.Delete(id);
            if (!resultView.isOk)
            {
                throw new Exception("ERROR: " + resultView.ErrorMessage);
            }

            return true;
        }

        public List<Core.Models.Clientes> Filter
        (
            string Identificacion, string Razon_Social,
            DateTime datetime_createdBetweenFrom, DateTime datetime_createdBetweenTo,
            DateTime datetime_createdEqual, DateTime datetime_createdMayor,
            DateTime datetime_createdMenor, string opeFecha
        )
        {
            ClientesDB clientesDB = new ClientesDB(StringConexion);
            List<Core.Models.Clientes> ListaClientes = clientesDB.Filtrar
                (Identificacion, Razon_Social,
                datetime_createdBetweenFrom, datetime_createdBetweenTo, datetime_createdEqual,
                datetime_createdMayor, datetime_createdMenor, opeFecha);

            return ListaClientes;
        }
    }
}
