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
        //private FrmAltaArticulos frmArticulos;
        private List<Articulos> listaArticulos;
        private object dgvArticulos;

        public pantallaInicial()
        {
            InitializeComponent();
        }

        private void pantallaInicial_Load(object sender, EventArgs e)
        {

            btnInicio_Click(null, e);
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

        //metodo para que se abra otra ventana en el panel contenedor
        private void AbrirFormHija(object FrmHija)
        {
            if (this.panelContenedor.Controls.Count > 0)
            {
                this.panelContenedor.Controls.RemoveAt(0);
            }
            Form FormAgregar = FrmHija as Form;
            FormAgregar.TopLevel = false;
            FormAgregar.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(FormAgregar);
            this.panelContenedor.Tag = FormAgregar;
            FormAgregar.Show();

            
        }

        //dandole funcionalidad a los botones del menu vertical
        
        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new FrmInicio());
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new FrmArticulos());
        }

        
    }
}
