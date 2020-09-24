using NR.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NR.Core.Interfaz
{
    public interface IClientes : IEntidadCrud<Clientes>
    {
        IEnumerable<SelectListItem> ListaSelect();
    }
}
