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
    public partial class ModificarDatosUsuario : Form
    {
        ClienteService clienteService;
        Cliente cliente;
        List<Cliente> clientes;

        public ModificarDatosUsuario()
        {
            clienteService = new ClienteService(ConfigConnection.ConnectionString);
            InitializeComponent();
            ConsultarYLlenarGridDeClientes();
        }
        //Mapear Cliente
        private Cliente MapearCliente()
        {
            cliente = new Cliente();
            cliente.Identificacion = textIdentificacion.Text;
            cliente.TipoDeIdentificacion = comboTipoId.Text;
            cliente.Nombres = textNombre.Text;
            cliente.Apellidos = textApellidos.Text;
            cliente.FechaDeNacimiento = dateTimeFechaDeNacimiento.Value;
            cliente.Edad = int.Parse(textEdad.Text);
            cliente.Sexo = comboSexo.Text;
            cliente.Telefono = textTelefono.Text;
            cliente.Direccion = textDireccion.Text;
            cliente.CorreoElectronico = textCorreo.Text;
            cliente.Peso = double.Parse(textPeso.Text);
            cliente.Altura = double.Parse(textAltura.Text);
            return cliente;
        }
        //Limpiar registro
        private void Limpiar()
        {
            textBuscar.Text = "";
            textIdentificacion.Text = "";
            comboTipoId.Text = "";
            textNombre.Text = "";
            textApellidos.Text = "";
            dateTimeFechaDeNacimiento.Value = DateTime.Today;
            comboSexo.Text = "";
            textTelefono.Text = "";
            textCorreo.Text = "";
            textPeso.Text = "";
            textAltura.Text = "";
            textIndMC.Text = "";
            textIndMO.Text = "";
            textIndGM.Text = "";
            textIndMM.Text = "";
            textIndMR.Text = "";
        }
        //Consulta Y llena en una tabla
        public void ConsultarYLlenarGridDeClientes()
        {
            ConsultaClienteRespuesta respuesta = new ConsultaClienteRespuesta();
            if (respuesta == null)
            {
                MessageBox.Show("No hay datos para mostrar");
            }
            else
            {
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
        }
        //Buscar por identificacion para llenar tabla<Buscar>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaClienteRespuesta respuesta = new BusquedaClienteRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                respuesta = clienteService.BuscarPorIdentificacion(id);
                var clientes = new List<Cliente> { respuesta.Cliente };
                dataGridView1.DataSource = clientes;
                if (respuesta.Cliente!=null)
                {
                    grupoDatosModificar.Enabled = true;
                    btnModificar.Enabled = true;
                }
                else
                {
                    if (respuesta.Cliente == null)
                    {
                        grupoDatosModificar.Enabled = false;
                        btnModificar.Enabled = false;
                        MessageBox.Show(respuesta.Mensaje, "Busqueda por identificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        //Calculos Cliente
        private void btnIndMC_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            string mensaje = clienteService.ValidarIndiceMasaCorporal(cliente);
            MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            cliente.ObtenerIndiceMasaCorporal();
            textIndMC.Text = cliente.ClasificacionPorPeso + ": " + cliente.IndiceDeMasaCorporal.ToString();
        }
        private void btnIndMO_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            string mensaje = clienteService.ValidarIndiceMasaOsea(cliente);
            MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            cliente.ObtenerIndiceMasaOsea();
            textIndMO.Text = cliente.IndiceDeMasaOsea.ToString();
        }
        private void btnIndGM_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            string mensaje = clienteService.ValidarIndiceGrasaMuscular(cliente);
            MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            cliente.ObtenerIndiceGrasaMuscular();
            textIndGM.Text = cliente.IndiceDeGrasaCorporal.ToString();
        }
        private void btnIndMM_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            string mensaje = clienteService.ValidarIndiceMasaMuscular(cliente);
            MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            cliente.ObtenerIndiceMasaMuscular();
            textIndMM.Text = cliente.IndiceDeMasaMuscular.ToString();
        }
        private void btnIndMR_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            string mensaje = clienteService.ValidarIndiceMasaResidual(cliente);
            MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            cliente.ObtenerIndiceMasaResidual();
            textIndMR.Text = cliente.IndiceDeMasaResidual.ToString();
        }
        //Validacion de llenado de datos
        private void textAltura_Validated(object sender, EventArgs e)
        {
            grupoCalculosUsuario.Enabled = true;
        }
        //Validacion de fecha de nacimiento y calculo de edad
        private void dateTimeFechaDeNacimiento_Validated(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            cliente.CalcularEdad();
            textEdad.Text = cliente.Edad.ToString();
        }
        //Control <Volver>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //<Modificar>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("Está seguro de Modificar la información", "Mensaje de Modificacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
            {
                Cliente cliente = MapearCliente();
                string mensaje = clienteService.Modificar(cliente);
                MessageBox.Show(mensaje, "Mensaje de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                ConsultarYLlenarGridDeClientes();
                Limpiar();
            }
        }
    }
}
