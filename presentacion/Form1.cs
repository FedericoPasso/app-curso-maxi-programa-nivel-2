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
using System.Runtime.InteropServices;//using para poder arrastrar la ventana

namespace presentacion
{
    public partial class pantallaInicial : Form
    {
        private List<Articulos> listaArticulos;
        public pantallaInicial()
        {
            InitializeComponent();
        }

        private void pantallaInicial_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            listaArticulos = articulo.listar();
            dgvArticulos.DataSource = listaArticulos;
            cargarImagen(listaArticulos[0].ImagenUrl);
            
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e) //metodo para que cambie la foto del picturebox cada vez que se selecciona una fila del data grid view
        {
            Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);
        }

        private void cargarImagen(string imagen) //metodo si en caso de que la imagen sea null en la db 
        {
            try
            {

                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxArticulos.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }



        //dandole funcionalidad a los nuevos botones 
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
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
