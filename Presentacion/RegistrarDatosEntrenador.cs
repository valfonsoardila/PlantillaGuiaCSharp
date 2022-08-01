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
    public partial class RegistrarDatosEntrenador : Form
    {
        EntrenadorService entrenadorService;
        Entrenador entrenador;
        public RegistrarDatosEntrenador()
        {
            entrenadorService = new EntrenadorService(ConfigConnection.ConnectionString);
            InitializeComponent();
        }
        //Funcion limpiara pantalla
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
        }
        //funcion que Mapea al Entrenador
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
            entrenador.TipoDeContrato = comboTipoDeContrato.Text;
            entrenador.MesesDeContrato = int.Parse(textMesesDeContrato.Text);
            entrenador.DiasDeTrabajo = int.Parse(textDiasDeTrabajo.Text);
            entrenador.HorasExtrasDeTrabajo = int.Parse(textHorasExtras.Text);
            return entrenador;
        }
        //Evento que calcula la edad
        private void dateTimeFechaDeNacimiento_Validated(object sender, EventArgs e)
        {
            Entrenador entrenador = MapearEntrenador();
            entrenador.CalcularEdad();
            textEdad.Text = entrenador.Edad.ToString();
        }
        //<Boton Generar Codigo>
        private void btnGenerarCodigo_Click(object sender, EventArgs e)
        {
            Entrenador entrenador = MapearEntrenador();
            entrenador.GenerarCodigoEntrenador();
            textGenerarCodigo.Text = entrenador.CodigoEntrenador;
        }
        //<Boton Calcular Pago>
        private void btnCalcularPago_Click(object sender, EventArgs e)
        {
            Entrenador entrenador = MapearEntrenador();
            entrenador.CalcularSalario();
            textCalcularPago.Text = entrenador.PagoDeSalario.ToString();
        }
        //<Boton Registrar Entrenador>
        private void btnRegistrarEntrenador_Click(object sender, EventArgs e)
        {
            Entrenador entrenador = MapearEntrenador();
            string mensaje = entrenadorService.Guardar(entrenador);
            MessageBox.Show(mensaje, "Mensaje de Guardado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            Limpiar();
        }
        //<Boton Volver>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
