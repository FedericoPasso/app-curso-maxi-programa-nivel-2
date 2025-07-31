using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class FrmAltaArticulos : Form
    {
        private Articulos articulos = null;
        public FrmAltaArticulos()
        {
            InitializeComponent();
        }
        //sobrecarga del constructor para usarlo a en la ventana para modificar articulos
        public FrmAltaArticulos(Articulos articulos)
        {
            InitializeComponent();
            this.articulos = articulos;
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulos nuevo = new Articulos();
            ArticuloNegocio negocio = new ArticuloNegocio();
            
            try
            {
                if(articulos == null)
                {
                    articulos = new Articulos();
                }
                articulos.Id = int.Parse(txtId.Text);
                articulos.Codigo = txtCodigo.Text;
                articulos.Nombre = txtNombre.Text;
                articulos.Descripcion = txtDescripcion.Text;
                articulos.ImagenUrl = txtImagen.Text;
                articulos.Precio = float.Parse(txtPrecio.Text);
                articulos.IdMarca = (Marcas)cboMarca.SelectedItem;
                articulos.IdCategoria = (Categorias)cboCategoria.SelectedItem;

                if(articulos.Id != 0)
                {
                    negocio.modificar(articulos);
                    MessageBox.Show("Modificado exitosamente");

                }
                negocio.agregar(articulos);
                MessageBox.Show("Agregado exitosamente");


                Close();
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
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                
                cboMarca.DataSource = marca.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";


                if(articulos != null)
                {
                    txtId.Text = articulos.Id.ToString();
                    txtCodigo.Text = articulos.Codigo;
                    txtNombre.Text = articulos.Nombre;
                    txtDescripcion.Text = articulos.Descripcion;
                    txtImagen.Text = articulos.ImagenUrl;
                    cargarImagen(articulos.ImagenUrl);   
                    txtPrecio.Text = articulos.Precio.ToString();
                    cboCategoria.SelectedValue = articulos.IdCategoria.Id;
                    cboMarca.SelectedValue = articulos.IdMarca.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)//metodo para cargar imagen por defecto si el usuario no carga una imagen
        {
            try
            {
                pbxAltaArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pbxAltaArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }
        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        //esto es para poder arrastrar la ventana ¯\_(ツ)_/¯
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);

        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
