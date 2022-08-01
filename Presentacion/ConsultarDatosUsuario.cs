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
    public partial class ConsultarDatosUsuario : Form
    {
        ClienteService clienteService;
        List<Cliente> clientes;

        public ConsultarDatosUsuario()
        {
            clienteService = new ClienteService(ConfigConnection.ConnectionString);
            InitializeComponent();
            ConsultarYLlenarGridDeClientes();
            comboSexo.Enabled = true;
            textTotal.Enabled = true;
            textTotalHombres.Enabled = true;
            textTotalMujeres.Enabled = true;
        }
        //funcion limpiar consulta
        private void Limpiar()
        {
            dataGridView1.DataSource = null;
            textBuscar.Text = "";
            textTotal.Text = "";
            textTotalMujeres.Text = "";
            textTotalHombres.Text = "";
        }
        //Consulta Y llena en una tabla
        public void ConsultarYLlenarGridDeClientes()
        {
            ConsultaClienteRespuesta respuesta = new ConsultaClienteRespuesta();
            string tipo = comboSexo.Text;
            if (tipo == "Todos")
            {
                textTotal.Enabled = true;
                textTotalHombres.Enabled = true;
                textTotalMujeres.Enabled = true;

                dataGridView1.DataSource = null;
                respuesta = clienteService.ConsultarTodos();
                clientes = respuesta.Clientes.ToList();
                dataGridView1.DataSource = respuesta.Clientes;

                textTotal.Text = clienteService.Totalizar().Cuenta.ToString();
                textTotalMujeres.Text = clienteService.TotalizarTipo("F").Cuenta.ToString();
                textTotalHombres.Text = clienteService.TotalizarTipo("M").Cuenta.ToString();
            }
            else
            {
                MessageBox.Show("No se hallan datos registrados ", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Filtrar por sexo
        private void comboSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select * from CLIENTE where Sexo='" + comboSexo.Text + "'";
            UpdateGrid(query, "CLIENTE");
            if (comboSexo.Text=="Todos")
            {
                ConsultarYLlenarGridDeClientes();
                textTotal.Enabled = true;
                textTotalHombres.Enabled = true;
                textTotalMujeres.Enabled = true;
            }
        }
        public void UpdateGrid(String query, String tbl)
        {
            SqlDataAdapter ada = new SqlDataAdapter(query, new SqlConnection(Properties.Settings.Default.YouGymConnectionString));
            DataSet dad = new DataSet();
            ada.Fill(dad, tbl);
            dataGridView1.DataSource = dad;
            dataGridView1.DataMember = tbl;
        }
        //Buscar por identificacion <Buscar>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Inhabilito el filtro
            comboSexo.Enabled = false;

            BusquedaClienteRespuesta respuesta = new BusquedaClienteRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                if (respuesta != null)
                {
                    respuesta = clienteService.BuscarPorIdentificacion(id);
                    var clientes = new List<Cliente> { respuesta.Cliente };
                    dataGridView1.DataSource = clientes;
                    textTotal.Text = clienteService.Totalizar().Cuenta.ToString();
                    textTotalMujeres.Text = clienteService.TotalizarTipo("F").Cuenta.ToString();
                    textTotalHombres.Text = clienteService.TotalizarTipo("M").Cuenta.ToString();
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
        //Llama a funcion <Limpia Consulta>
        private void btnLimpiarConsulta_Click(object sender, EventArgs e)
        {
            Limpiar();
            comboSexo.Enabled = true;
            comboSexo.Text = "Todos";
        }
        //Boton de control <Volver>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
