using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouGym;
using Logica;
using Entidades;

namespace YouGym
{
    public partial class RegistrarDatosUsuario : Form
    {
        ClienteService clienteService;
        Cliente cliente;
        public RegistrarDatosUsuario()
        {
            InitializeComponent();
            clienteService = new ClienteService(ConfigConnection.ConnectionString);
        }
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
            cliente.CorreoElectronico=textCorreo.Text;
            cliente.Peso = double.Parse(textPeso.Text);
            cliente.Altura = double.Parse(textAltura.Text);
            return cliente;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            string mensaje = clienteService.Guardar(cliente);
            MessageBox.Show(mensaje, "Mensaje de Guardado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            Limpiar();
        }
        private void Limpiar()
        {
            textIdentificacion.Text = "";
            comboTipoId.Text = "";
            textNombre.Text = "";
            textApellidos.Text = "";
            dateTimeFechaDeNacimiento.Value = DateTime.Today;
            textEdad.Text = "";
            comboSexo.Text = "";
            textDireccion.Text = "";
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
        private void btnGeberarCodigo_Click(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            cliente.GenerarCodigoCliente();
            textGenerarCodigo.Text = cliente.CodigoCliente;
        }
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
        private void btnIndGM_Click_1(object sender, EventArgs e)
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
        private void dateTimeFechaDeNacimiento_Validated(object sender, EventArgs e)
        {
            Cliente cliente = MapearCliente();
            cliente.CalcularEdad();
            textEdad.Text = cliente.Edad.ToString();
        }

        private void textAltura_Validated(object sender, EventArgs e)
        {
            grupoCalculos.Enabled = true;
            textGenerarCodigo.Enabled = true;
            btnGenerarCodigo.Enabled = true;
            btnRegistrarCliente.Enabled = true;
        }
        private void textGenerarCodigo_Validated(object sender, EventArgs e)
        {
            textIndMC.Enabled = true;
            btnIndMC.Enabled = true;
        }
    }
}