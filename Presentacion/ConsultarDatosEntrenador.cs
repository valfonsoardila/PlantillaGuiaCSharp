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
using System.Data.SqlClient;

namespace YouGym
{
    public partial class ConsultarDatosEntrenador : Form
    {
        private EntrenadorService entrenadorService;
        private List<Entrenador> entrenadores; 
        public ConsultarDatosEntrenador()
        {
            entrenadorService = new EntrenadorService(ConfigConnection.ConnectionString);
            InitializeComponent();
            ConsultarYLlenarGridDeEntrenadores();
            comboSexo.Enabled = true;
            textTotalEntrenadores.Enabled = true;
            textTotalHombresEntrenadores.Enabled = true;
            textTotalMujeresEntrenadoras.Enabled = true;
        }
        //funcion limpiar consulta
        private void Limpiar()
        {
            dataGridViewEntrenadores.DataSource = null;
            textBuscar.Text = "";
            textTotalEntrenadores.Text = "";
            textTotalMujeresEntrenadoras.Text = "";
            textTotalHombresEntrenadores.Text = "";
        }
        //Consulta Y llena en una tabla
        private void ConsultarYLlenarGridDeEntrenadores()
        {
            ConsultaEntrenadorRespuesta respuesta = new ConsultaEntrenadorRespuesta();
            string tipo = comboSexo.Text;
            if (tipo == "Todos")
            {
                if (respuesta == null)
                {
                    MessageBox.Show("No hay datos para mostrar");
                }
                else
                {
                    if (respuesta != null)
                    {
                        textTotalEntrenadores.Enabled = true;
                        textTotalHombresEntrenadores.Enabled = true;
                        textTotalMujeresEntrenadoras.Enabled = true;
                        dataGridViewEntrenadores.DataSource = null;
                        respuesta = entrenadorService.ConsultarTodos();
                        entrenadores = respuesta.Entrenadores.ToList();
                        dataGridViewEntrenadores.DataSource = respuesta.Entrenadores;
                        textTotalEntrenadores.Text = entrenadorService.Totalizar().Cuenta.ToString();
                        textTotalMujeresEntrenadoras.Text = entrenadorService.TotalizarTipo("F").Cuenta.ToString();
                        textTotalHombresEntrenadores.Text = entrenadorService.TotalizarTipo("M").Cuenta.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("No se hallan datos registrados ", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Filtrar por sexo
        private void comboSexo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            String query = "select * from ENTRENADOR where Sexo='" + comboSexo.Text + "'";
            UpdateGrid(query, "ENTRENADOR");
            if (comboSexo.Text == "Todos")
            {
                ConsultarYLlenarGridDeEntrenadores();
                textTotalEntrenadores.Enabled = true;
                textTotalHombresEntrenadores.Enabled = true;
                textTotalMujeresEntrenadoras.Enabled = true;
            }
        }
        public void UpdateGrid(String query, String tbl)
        {
            SqlDataAdapter ada = new SqlDataAdapter(query, new SqlConnection(Properties.Settings.Default.YouGymConnectionString));
            DataSet dad = new DataSet();
            ada.Fill(dad, tbl);
            dataGridViewEntrenadores.DataSource = dad;
            dataGridViewEntrenadores.DataMember = tbl;
        }
        //Buscar por identificacion <Buscar>
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            //Inhabilito el filtro
            comboSexo.Enabled = false;
            BusquedaEntrenadorRespuesta respuesta = new BusquedaEntrenadorRespuesta();
            string identificacion = textBuscar.Text;
            if (identificacion != "")
            {
                if (respuesta != null)
                {
                    respuesta = entrenadorService.BuscarPorIdentificacion(identificacion);
                    var entrenadores = new List<Entrenador> { respuesta.Entrenador };
                    dataGridViewEntrenadores.DataSource = entrenadores;
                }
                else
                {
                    if (respuesta == null)
                    {
                        MessageBox.Show(respuesta.Mensaje, "Busqueda por identificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        //Llama a funcion <Limpia Consulta>
        private void btnLimpiarConsulta_Click(object sender, EventArgs e)
        {
            Limpiar();
            comboSexo.Enabled = true;
            comboSexo.Text = "Todos";
        }
        //Boton de control <Volver>
        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}