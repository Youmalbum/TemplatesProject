using System;
using System.Collections.Generic;
using System.Text;

namespace NR.Driver
{
    public class DefaultConnectionStringDataBase
    {
        public string StringConexion
        {
            get
            {
                return "Server=SERVER\\SQLEXPRESS;Database=ventasdb;Trusted_Connection=True";
            }
        }
    }
}
