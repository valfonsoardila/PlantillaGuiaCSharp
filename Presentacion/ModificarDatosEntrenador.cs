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
    public partial class ModificarDatosEntrenador : Form
    {
        EntrenadorService entrenadorService;
        Entrenador entrenador;
        List<Entrenador> entrenadores;
        public ModificarDatosEntrenador()
        {
            entrenadorService = new EntrenadorService(ConfigConnection.ConnectionString);
            InitializeComponent();
            ConsultarYLlenarGridDeEntrenadores();
        }
        //Mapear Entrenador
        private Entrenador MapearEntrenador()
        {
            entrenador = new Entrenador();
            entrenador.Identificacion = textIdentificacion.Text;
            entrenador.TipoDeIdentificacion = comboTipoId.Text;
            entrenador.Nombres = textNombre.Text;
            entrenador.Apellidos = textApellidos.Text;
            entrenador.FechaDeNacimiento = dateTimeFechaDeNacimiento.Value;
            entrenador.Edad = int.Parse(textEdad.Text);
            entrenador.Sexo = comboSexo.Text;
            entrenador.Direccion = textDireccion.Text;
            entrenador.Telefono = textTelefono.Text;
            entrenador.CorreoElectronico = textCorreo.Text;
            return entrenador;
        }
        //Limpiar registro
        private void Limpiar()
        {
            textIdentificacion.Text = "";
            comboTipoId.Text = "";
            textNombre.Text = "";
            textApellidos.Text = "";
            dateTimeFechaDeNacimiento.Value = DateTime.Today;
            comboSexo.Text = "";
            textDireccion.Text = "";
            textTelefono.Text = "";
            textCorreo.Text = "";
        }
        //Consulta y llena la tabla
        private void ConsultarYLlenarGridDeEntrenadores()
        {
            ConsultaEntrenadorRespuesta respuesta = new ConsultaEntrenadorRespuesta();
            if (respuesta == null)
            {
                MessageBox.Show("No hay datos para mostrar");
            }
            else
            {
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
        }
        //Buscar por identificacion para llenar tabla<Buscar>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaEntrenadorRespuesta respuesta = new BusquedaEntrenadorRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                respuesta = entrenadorService.BuscarPorIdentificacion(id);
                var entrenadores = new List<Entrenador> { respuesta.Entrenador };
                dataGridView1.DataSource = entrenadores;
                if (respuesta.Entrenador != null)
                {
                    grupoDatosModificar.Enabled = true;
                    btnModificar.Enabled = true;
                }
                else
                {
                    if (respuesta.Entrenador == null)
                    {
                        grupoDatosModificar.Enabled = false;
                        btnModificar.Enabled = false;
                        MessageBox.Show(respuesta.Mensaje, "Busqueda por identificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        //Validacion de llenado de datos
        private void textCorreo_Validated(object sender, EventArgs e)
        {

        }
        //Validacion de fecha de nacimiento y calculo de edad
        private void dateTimeFechaDeNacimiento_Validated(object sender, EventArgs e)
        {
            Entrenador entrenador = MapearEntrenador();
            entrenador.CalcularEdad();
            textEdad.Text = entrenador.Edad.ToString();
        }
        //<Boton Modificar>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("Está seguro de Modificar la información", "Mensaje de Modificacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
            {
                Entrenador entrenador = MapearEntrenador();
                string mensaje = entrenadorService.Modificar(entrenador);
                MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ConsultarYLlenarGridDeEntrenadores();
                Limpiar();
            }
        }
    }
}
