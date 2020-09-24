using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NR.Core.Models
{
    public class Clientes
    {
        [Key]
        public int cliente_id { get; set; }

        public string razon_social { get; set; }

        public DateTime fecha_reg { get; set; }

        //public List<DetallesFactura> detalles { get; set; }

        //public <DocumentosFactura> documentos { get; set; }

    }
}
