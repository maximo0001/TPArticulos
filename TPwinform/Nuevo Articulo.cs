using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Funcionalidad;

namespace TPwinform
{
    public partial class Nuevo_Articulo : Form
    {
        private Articulo articulo = null;
        
        public Nuevo_Articulo()
        {
            InitializeComponent();
        }
        public Nuevo_Articulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }

        private void Nuevo_Articulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marcaNegocio.Listar();

                cboCategoria.DataSource = categoriaNegocio.Listar();

                if(articulo != null)
                {
                    txtCodigoArticulo.Text = articulo.CodigoArticulo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboMarca.SelectedItem = articulo.Marca;
                    cboCategoria.SelectedItem = articulo.Categoria;
                    txtUrlImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    txtPrecio.Text = articulo.Precio.ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.CodigoArticulo = txtCodigoArticulo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.UrlImagen = txtUrlImagen.Text;
                articulo.Precio = float.Parse(txtPrecio.Text);
                if(articulo.Id != 0)
                {
                    negocio.Modificar(articulo);
                    MessageBox.Show("Modificado con exito");

                }
                else
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado con exito");
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
