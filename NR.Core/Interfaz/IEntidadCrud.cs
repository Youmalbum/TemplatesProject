using NR.Core.ModelsView;
using System;
using System.Collections.Generic;
using System.Text;

namespace NR.Core.Interfaz
{
    public interface IEntidadCrud<T>
    {
        List<T> GetAll();
        List<T> GetAll(int Index, int TopReg);
        T Get(int id);
       ResultView Add(T entity);
       ResultView Update(T entity);
       ResultView Delete(int id);
    }
}
