using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica;
using Entidades;

namespace YouGym
{
    public partial class EliminarDatosEntrenador : Form
    {
        EntrenadorService entrenadorService;
        List<Entrenador> entrenadores;
        public EliminarDatosEntrenador()
        {
            entrenadorService = new EntrenadorService(ConfigConnection.ConnectionString);
            InitializeComponent();
            ConsultarYLlenarGridDeEntrenador();
        }
        public void ConsultarYLlenarGridDeEntrenador()
        {
            ConsultaEntrenadorRespuesta respuesta = new ConsultaEntrenadorRespuesta();
            if (respuesta != null)
            {

                dataGridView1.DataSource = null;
                respuesta = entrenadorService.ConsultarTodos();
                entrenadores = respuesta.Entrenadores.ToList();
                dataGridView1.DataSource = respuesta.Entrenadores;

            }
            else
            {
                MessageBox.Show("No se hallan datos registrados ", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //<Boton Buscar>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaEntrenadorRespuesta respuesta = new BusquedaEntrenadorRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                if (respuesta != null)
                {
                    respuesta = entrenadorService.BuscarPorIdentificacion(id);
                    var entrenadores = new List<Entrenador> { respuesta.Entrenador };
                    dataGridView1.DataSource = entrenadores;
                }
                else
                {
                    if (respuesta == null)
                    {
                        MessageBox.Show("No hay datos para mostrar");
                    }
                }
            }
        }
        //<boton volver>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //<boton Eliminar Datos>
        private void btnEliminarDatos_Click(object sender, EventArgs e)
        {
            string id = textBuscar.Text;
            if (id != "")
            {
                var respuesta = MessageBox.Show("Está seguro de eliminar la información", "Mensaje de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    string mensaje = entrenadorService.Eliminar(id);
                    MessageBox.Show(mensaje, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor digite la cedula de la persona a modificar y presione el boton buscar", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
