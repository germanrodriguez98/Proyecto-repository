using System.Collections.Generic;

namespace Proyecto32
{
    public class ProductoController
    {
        private readonly IRepository<Producto> _repo;

        public ProductoController(IRepository<Producto> repo)
        {
            _repo = repo;
        }

        public void Agregar(string nombre, int stock, decimal precio)
        {
            _repo.Agregar(new Producto { Nombre = nombre, Precio = precio, Stock = stock });
        }

        public List<Producto> ObtenerTodos() => _repo.LeerTodos();

        public void Eliminar(int id) => _repo.Eliminar(id);

        public void Modificar(Producto modificado) => _repo.Actualizar(modificado);

        public List<Producto> Buscar(string texto)
        {
            List<Producto> resultados = new List<Producto>();
            foreach (Producto p in _repo.LeerTodos())
            {
                if (p.Nombre.ToLower().Contains(texto.ToLower()))
                {
                    resultados.Add(p);
                }
            }
            return resultados;
        }
        public Producto ObtenerPorId(int id) => _repo.BuscarPorId(id);
    }
}