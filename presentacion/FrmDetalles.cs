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
    public partial class FrmDetalles : Form
    {
        private Articulos articulo = null;
        private ArticuloNegocio negocio = new ArticuloNegocio();
        public FrmDetalles()
        {
            InitializeComponent();
        }
        public FrmDetalles(Articulos articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void FrmDetalles_Load(object sender, EventArgs e)
        {
            try
            {            
               
                lblDetalleCodigo.Text = articulo.Codigo;
                lblDetalleNombre.Text = articulo.Nombre;
                lblDetalleDescripcion.Text = articulo.Descripcion;
                cargarImagen(articulo.ImagenUrl);
                lblDetallePrecio.Text = articulo.Precio.ToString();
                lblDetalleMarca.Text = articulo.IdMarca.Descripcion;
                lblDetalleCategoria.Text = articulo.IdCategoria.Descripcion;    


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
    
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxDetalleArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxDetalleArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //esto es para poder arrastrar la ventana ¯\_(ツ)_/¯
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
