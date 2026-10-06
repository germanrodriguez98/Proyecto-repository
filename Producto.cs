namespace Proyecto32
{
    public class Producto : IEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
    }
}