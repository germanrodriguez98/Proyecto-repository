using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Proyecto32
{
    public class JsonRepository<T> : IRepository<T>
        where T : IEntidad
    {
        private readonly string _ruta;

        public JsonRepository(string ruta)
        {
            _ruta = ruta;
        }

        public List<T> LeerTodos()
        {
            if (!File.Exists(_ruta))
                return new List<T>();

            try
            {
                string json = File.ReadAllText(_ruta);

                return JsonSerializer.Deserialize<List<T>>(json)
                       ?? new List<T>();
            }
            catch (JsonException ex)
            {
                throw new Exception(
                    "Archivo JSON dañado: " + ex.Message,
                    ex
                );
            }
        }

        private void Persistir(List<T> lista)
        {
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(lista, opciones);

            string directorio = Path.GetDirectoryName(_ruta);

            if (!string.IsNullOrEmpty(directorio))
                Directory.CreateDirectory(directorio);

            File.WriteAllText(_ruta, json);
        }

        public void Agregar(T item)
        {
            var lista = LeerTodos();

            item.Id = lista.Count > 0
                ? lista.Max(e => e.Id) + 1
                : 1;

            lista.Add(item);

            Persistir(lista);
        }

        public T BuscarPorId(int id)
        {
            return LeerTodos()
                .FirstOrDefault(e => e.Id == id);
        }

        public void Actualizar(T item)
        {
            var lista = LeerTodos();

            int indice = lista.FindIndex(
                e => e.Id == item.Id
            );

            if (indice >= 0)
            {
                lista[indice] = item;
                Persistir(lista);
            }
        }

        public void Eliminar(int id)
        {
            var lista = LeerTodos();

            int eliminados = lista.RemoveAll(
                e => e.Id == id
            );

            if (eliminados > 0)
                Persistir(lista);
        }
    }
}