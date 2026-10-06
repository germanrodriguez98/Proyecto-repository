using System;
using System.Windows.Forms;

namespace Proyecto32
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var repo = new JsonRepository<Producto>("productos.json");
            var controller = new ProductoController(repo);
            var form = new Form1(controller);
             Application.Run(form);
}
    }
}