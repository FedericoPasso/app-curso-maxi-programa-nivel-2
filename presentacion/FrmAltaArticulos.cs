using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace presentacion
{
    public partial class FrmAltaArticulos : Form
    {
        public FrmAltaArticulos()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulos nuevo = new Articulos();
            ArticuloNegocio negocio = new ArticuloNegocio();
            
            try
            {
                nuevo.Id = int.Parse(txtId.Text);
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenUrl = txtImagen.Text;
                nuevo.Precio = float.Parse(txtPrecio.Text);
                nuevo.IdMarca = (Marcas)cboMarca.SelectedItem;
                nuevo.IdCategoria = (Categorias)cboCategoria.SelectedItem;

                negocio.agregar(nuevo);
                MessageBox.Show("Agregado exitosamente");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void FrmAltaArticulos_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = categoria.listar();
                cboMarca.DataSource = marca.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
