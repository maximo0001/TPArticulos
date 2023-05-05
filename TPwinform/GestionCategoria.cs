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
    public partial class frmGestionCategoria : Form
    {
        
        public frmGestionCategoria()
        {
            InitializeComponent();
        }
        private List<Categoria> listaCategoria;
        private void frmGestionCategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                listaCategoria = negocio.Listar();
                dgvCategoria.DataSource = listaCategoria;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria();

            try
            {
                categoria.Descripcion = txtDescripcion.Text;
                negocio.Agregar(categoria);
                MessageBox.Show("Modificado con exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Categoria)dgvCategoria.CurrentRow.DataBoundItem;
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
