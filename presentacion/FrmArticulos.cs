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
    public partial class FrmArticulos : Form
    {
        private List<Articulos> listaArticulos;
        public FrmArticulos()
        {
            InitializeComponent();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
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







    }
}
