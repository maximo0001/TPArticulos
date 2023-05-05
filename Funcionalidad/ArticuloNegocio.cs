using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace Funcionalidad
{
    //imito el nombre de clase de los videos porque no se como ponerle
    public class ArticuloNegocio
    {
       public List<Articulo> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();
            try
            {
                datos.setearConsulta("select a.Id, Codigo, Nombre, a.Descripcion, m.Id IdMarcas, c.Id IdCategorias, ImagenUrl, Precio from ARTICULOS a, MARCAS m, CATEGORIAS c,IMAGENES i where m.Id = a.IdMarca and c.Id = a.IdCategoria and i.IdArticulo = a.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarcas"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategorias"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (float)(decimal)datos.Lector["Precio"];    

                    lista.Add(aux);
                }
                datos.cerrarConexion();
        
                return lista;
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

        //
        //Agregar
        //          -falta hacer en el form
        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values(@codigo, @nombre, @descripcion, @marca, @categoria, @precio) " +
                    "insert into IMAGENES (IdArticulo, ImagenUrl) select a.id, @imagen from  ARTICULOS a where Codigo = @codigo");
                datos.setearParametro("@codigo", nuevo.CodigoArticulo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@marca", nuevo.Marca);
                datos.setearParametro("@categoria", nuevo.Categoria);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.setearParametro("@imagen", nuevo.UrlImagen);
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
        //
        //ELIMINAR
        //          -falta hacer en el form
        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ARTICULOS where id = @id delete from IMAGENES where IdArticulo = @id");
                datos.setearParametro("@id", id);
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

        //
        //  MODIFICAR
        //          -falta hacer en el form
        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set codigo = @codigo, nombre = @nombre, descripcion = @descripcion, idMarca = @idMarca, idCategoria = @idCategoria, precio = @precio where id = @id update IMAGENES set ImagenUrl = @imagen where IdArticulo = @id");
                datos.setearParametro("@codigo", articulo.CodigoArticulo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.Id);
                datos.setearParametro("@imagen", articulo.UrlImagen);

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

        public Articulo Leer(int id)
        {
            Articulo articulo = new Articulo();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("select a.Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio, ImagenUrl from ARTICULOS a, IMAGENES i where a.id = @id and i.IdArticulo = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.CodigoArticulo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.UrlImagen = (string)datos.Lector["ImagenUrl"];
                }

                return articulo;
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
    }

}
