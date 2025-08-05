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

            cboCampo.Items.Add("Id");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
        }
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Id")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else 
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
                
            }
         
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
                ocultarColumnas();
                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["imagenUrl"].Visible = false;
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
       
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            Articulos seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar el articulo?", "Eliminar Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);//sobrecarga del messageBox para verificar la accion del boton
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                    articulo.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        
       
        private void txtFiltrar_TextChanged(object sender, EventArgs e)
        {
            List<Articulos> listaFiltrada;
            string filtro = txtFiltrar.Text;
            if (filtro.Length >= 3)
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.IdMarca.Descripcion.ToUpper().Contains(filtro.ToUpper()));//ciclo que funciona como un foreach para almacenar los datos filtrados
            }
            else
            {
                listaFiltrada = listaArticulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        //METODO PARA USAR ELIMINACION FISICA O LOGICA
        //private void eliminar(bool logico = false)
        //{
        //    ArticuloNegocio articulo = new ArticuloNegocio();
        //    Articulos seleccionado;
        //
        //    try
        //    {
        //        DialogResult respuesta = MessageBox.Show("¿Desea eliminar el articulo?", "Eliminar Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);//sobrecarga del messageBox para verificar la accion del boton
        //        if (respuesta == DialogResult.Yes)
        //        {
        //            seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
        //            if (logico)
        //                articulo.eliminar(seleccionado.Id);
        //            else
        //            {
        //                articulo.eliminarLogico(seleccionado.Id);
        //            }
        //                cargar();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        private void dgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)//el anterior metodo para cambiar imagenes dejó de funcionar asi que usé este
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

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();  
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch ( Exception ex)
            {
                //if (cboCampo == "Id")
                //{
                //
                //}
                MessageBox.Show("Por favor complete todos los campos");
            }
        }
    }    
}
