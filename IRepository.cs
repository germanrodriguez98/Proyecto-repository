using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto32
{
    public interface IRepository<T> where T : IEntidad
    {
        List<T> LeerTodos();
        T BuscarPorId(int id);
        void Agregar(T item);
        void Actualizar(T item);
        void Eliminar(int id);
    }
}
