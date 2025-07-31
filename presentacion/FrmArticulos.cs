using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace presentacion
{
    public partial class FrmArticulos : Form
    {
        private pantallaInicial panel;
        private List<Articulos> listaArticulos;
        public FrmArticulos()
        {
            InitializeComponent();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
        }

        // private void dgvArticulos_SelectionChanged(object sender, EventArgs e) //metodo para que cambie la foto del picturebox cada vez que se selecciona una fila del data grid view
        // {
        //     Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
        //     cargarImagen(seleccionado.ImagenUrl);
        // }





        private void cargarImagen(string imagen) //metodo para cargar imagen por defecto en caso de que la imagen sea null en la db 
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
        public void cargar()
        {

            ArticuloNegocio articulo = new ArticuloNegocio();
            try
            {

                listaArticulos = articulo.listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["imagenUrl"].Visible = false;
                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulos agregar = new FrmAltaArticulos();
            agregar.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulos seleccionado;
            seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            
        
            FrmAltaArticulos modificar = new FrmAltaArticulos(seleccionado);
            modificar.ShowDialog();
            cargar();
        }
       
        
        private void dgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)//el anterior metodo para cargar imagenes dejó de funcionar asi que usé este
        {
            if (e.RowIndex >= 0)
            {
                string url = dgvArticulos.Rows[e.RowIndex].Cells["ImagenUrl"].Value.ToString();

                if (url.StartsWith("http"))
                {
                    // Descargar imagen desde internet
                    using (WebClient wc = new WebClient())
                    {
                        byte[] datos = wc.DownloadData(url);
                        using (MemoryStream ms = new MemoryStream(datos))
                        {
                            pbxArticulos.Image = Image.FromStream(ms);
                        }
                    }
                }
                else if (System.IO.File.Exists(url))
                {
                    // Cargar imagen local
                    pbxArticulos.Image = Image.FromFile(url);
                }
                else
                {
                    pbxArticulos.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
                    //pbxArticulos.Image = null; // Imagen no encontrada
                }

                pbxArticulos.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

    }    
}
