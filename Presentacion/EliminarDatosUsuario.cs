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
    public partial class EliminarDatosUsuario : Form
    {
        ClienteService clienteService;
        List<Cliente> clientes;
        public EliminarDatosUsuario()
        {
            clienteService = new ClienteService(ConfigConnection.ConnectionString);
            InitializeComponent();
            ConsultarYLlenarGridDeClientes();
        }
        private void Limpiar()
        {
            dataGridView1.DataSource = null;
            textBuscar.Text = "";
        }
        public void ConsultarYLlenarGridDeClientes()
        {
            ConsultaClienteRespuesta respuesta = new ConsultaClienteRespuesta();
            if (respuesta != null)
            {

                dataGridView1.DataSource = null;
                respuesta = clienteService.ConsultarTodos();
                clientes = respuesta.Clientes.ToList();
                dataGridView1.DataSource = respuesta.Clientes;

            }
            else
            {
                MessageBox.Show("No se hallan datos registrados ", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaClienteRespuesta respuesta = new BusquedaClienteRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                respuesta = clienteService.BuscarPorIdentificacion(id);
                var clientes = new List<Cliente> { respuesta.Cliente };
                dataGridView1.DataSource = clientes;
            }
        }
        //Llama a funcion <Limpia Consulta>
        private void btnLimpiarConsulta_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            BusquedaClienteRespuesta respuesta = new BusquedaClienteRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                if (respuesta != null)
                {
                    respuesta = clienteService.BuscarPorIdentificacion(id);
                    var clientes = new List<Cliente> { respuesta.Cliente };
                    dataGridView1.DataSource = clientes;
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
        private void btnEliminarDatos_Click(object sender, EventArgs e)
        {
            string id = textBuscar.Text;
            if (id != "")
            {
                var respuesta = MessageBox.Show("Está seguro de eliminar la información", "Mensaje de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    string mensaje = clienteService.Eliminar(id);
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
