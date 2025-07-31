using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulos> listar()
        {
            List<Articulos> listaArticulos = new List<Articulos>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();//devuelve un objeto SQLDATAREADER que queda almacenado en la variable lector.
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database= CATALOGO_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select A.Id, Codigo as CodArticulo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl as Imagen, Precio, IdMarca, IdCategoria From ARTICULOS A, CATEGORIAS C, MARCAS M Where C.Id = A.IdCategoria And M.Id = A.IdMarca";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();//despues de establecer la consulta se escriben los datos en la variable lector   

                while (lector.Read())//recorre linea por linea la tabla de la db
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["CodArticulo"]; //hace la consulta con el alias que le pusimos 
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    if (!(lector["Imagen"] is DBNull))//condicion en caso que la imagen sea nula en la db
                        aux.ImagenUrl = (string)lector["Imagen"];
                    
                    aux.IdMarca = new Marcas(); //se crea un objeto de la clase marcas para poder usarlo en la consulta 
                    aux.IdMarca.Id = (int)lector["IdMarca"];
                    aux.IdMarca.Descripcion = (string)lector["Marca"];

                    aux.IdCategoria = new Categorias();
                    aux.IdCategoria.Id = (int)lector["IdCategoria"];
                    aux.IdCategoria.Descripcion = (string)lector["Categoria"];
                    aux.Precio = (float)(decimal)lector["Precio"];
                
                    listaArticulos.Add(aux);    
                }

                conexion.Close();
                return listaArticulos;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public void agregar(Articulos nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, IdMarca, IdCategoria, Precio) Values (@Codigo, @Nombre, @Descripcion, @Imagen, @IdMarca, @IdCategoria, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Imagen", nuevo.ImagenUrl);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.Id);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void modificar (Articulos art)
        {
            AccesoDatos datos = new AccesoDatos ();
            try
            {
                datos.setearConsulta("Update ARTICULOS Set Codigo = @Codigo ,Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @Imagen, Precio = @Precio Where Id = @Id");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@Imagen", art.ImagenUrl);
                datos.setearParametro("@idMarca", art.IdMarca.Id);
                datos.setearParametro("@IdCategoria", art.IdCategoria.Id);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@Id", art.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex; 
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete From ARTICULOS Where Id = @Id");
                datos.setearParametro("@Id", id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }
        
    
    
    }
}
