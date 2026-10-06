using System;
using System.Windows.Forms;

namespace Proyecto32
{
    public partial class FormDetalle : Form
    {
        private int _id;

        public FormDetalle(Producto producto)
        {
            InitializeComponent();

            _id = producto.Id;
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString();
            nudStock.Value = producto.Stock;
        }

        public Producto ProductoModificado => new Producto
        {
            Id = _id,
            Nombre = txtNombre.Text,
            Precio = decimal.Parse(txtPrecio.Text),
            Stock = (int)nudStock.Value
        };

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre no puede estar vacio.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal _))
            {
                MessageBox.Show("El precio debe ser un numero.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void nudStock_ValueChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}