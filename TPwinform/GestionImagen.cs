using dominio;
using Funcionalidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPwinform
{
    public partial class frmGestionImagen : Form
    {
        public frmGestionImagen()
        {
            InitializeComponent();
        }
        private List<Imagen> listaImagen;
        private void GestionImagen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            try
            {
                ImagenNegocio negocio = new ImagenNegocio();
                listaImagen = negocio.Listar();
                dgvImagen.DataSource = listaImagen;
                cargarImagen(listaImagen[0].UrlImagen);

                dgvImagen.Columns["Id"].Visible = false;
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
        
       
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ImagenNegocio negocio = new ImagenNegocio();
            Imagen seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Imagen)dgvImagen.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.id);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ImagenNegocio negocio = new ImagenNegocio();
            Imagen imagen = new Imagen();

            try
            {
                imagen.idArticulo = int.Parse(txtIdArticulo.Text);
                imagen.UrlImagen = txtUrl.Text;

                negocio.Agregar(imagen);
                MessageBox.Show("Modificado con exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvImagen_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvImagen.CurrentRow != null)
            {
                Imagen seleccionado = (Imagen)dgvImagen.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }
    }
}
