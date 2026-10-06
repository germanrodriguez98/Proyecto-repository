using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto32
{
    public partial class Form1 : Form
    {
        private readonly ProductoController _controller;

        public Form1(ProductoController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarLista();
        }

        // Método para refrescar el DataGridView y la etiqueta del contador
        private void CargarLista()
        {
            dataGridView1.DataSource = null;
            List<Producto> productos = _controller.ObtenerTodos();
            dataGridView1.DataSource = productos;

            lblContador.Text = productos.Count + " productos";
        }

        // Evento para abrir el formulario de agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (FormAgregar f = new FormAgregar())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _controller.Agregar(f.NombreProducto, f.StockProducto, f.PrecioProducto);
                        CargarLista();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Evento para abrir el formulario de edición
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un producto de la lista.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            Producto producto = _controller.ObtenerPorId(id);

            if (producto == null)
            {
                MessageBox.Show("No se encontró el producto seleccionado.");
                return;
            }

            using (FormDetalle f = new FormDetalle(producto))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _controller.Modificar(f.ProductoModificado);
                        CargarLista();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Evento para eliminar un producto
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Seguro que deseas eliminar este producto?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    _controller.Eliminar(id);
                    CargarLista();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Evento para realizar la búsqueda
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarProducto.Text))
            {
                CargarLista();
            }
            else
            {
                List<Producto> resultados = _controller.Buscar(txtBuscarProducto.Text);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = resultados;
                lblContador.Text = resultados.Count + " productos";
            }
        }

        // Evento para limpiar la búsqueda
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtBuscarProducto.Clear();
            CargarLista();
        }
    }
}