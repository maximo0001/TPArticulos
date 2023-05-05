using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using Funcionalidad;

namespace TPwinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<Articulo> listaArticulos;

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            
        }

        private void cargar()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.Listar();
                dgvArticulo.DataSource = listaArticulos;
                cargarImagen(listaArticulos[0].UrlImagen);

                dgvArticulo.Columns["Id"].Visible = false;
                dgvArticulo.Columns["UrlImagen"].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            
            if(dgvArticulo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
            
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Nuevo_Articulo nuevo = new Nuevo_Articulo();
            nuevo.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                }
                else
                    Close();
                cargar();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltro;
            string filtro = txtFiltro.Text;

            if (filtro != "")
                listaFiltro = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.CodigoArticulo.ToUpper().Contains(filtro.ToUpper()));
            else
            {
                listaFiltro = listaArticulos;
            }

            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listaFiltro;
            dgvArticulo.Columns["Id"].Visible = false;
            dgvArticulo.Columns["UrlImagen"].Visible = false;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

            Nuevo_Articulo modificar = new Nuevo_Articulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }
    }
}
