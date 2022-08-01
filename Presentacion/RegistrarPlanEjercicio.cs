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
    public partial class RegistrarPlanEjercicio : Form
    {
        PlanDeEjercicioService planDeEjercicioService;
        PlanDeEjercicio planDeEjercicio;
        readonly ClienteService clienteService;
        readonly Cliente cliente;
        public RegistrarPlanEjercicio()
        {
            planDeEjercicioService = new PlanDeEjercicioService(ConfigConnection.ConnectionString);
            clienteService = new ClienteService(ConfigConnection.ConnectionString);
            InitializeComponent();
        }
        //Limpiar registro
        private void Limpiar() 
        {
            textBuscar.Text = "";
            dataGridView1.DataSource = null;
            grupoDatosDeEntrenamiento.Enabled = false;
            labelIdPlanEjercicio.Text = "*            ";
            comboTipoTiempo.Text = "";
            textNumeroDiasMeses.Text = "";
            dateTimeFechaDeEntreno.Value = DateTime.Today;
            comboSesion.Text = "";
            textObjetivo.Text = "";
            comboEstado.Text = "";
            comboCiclo.Text = "";
            labelPrecio.Text = "*            ";
            textDescripcionPlan.Text = "";
        }
        //Mapeamos el plan de ejercicio
        private PlanDeEjercicio MapearPlanEjercicio()
        {
            planDeEjercicio = new PlanDeEjercicio();
            planDeEjercicio.TipoDeTiempo = comboTipoTiempo.Text;
            planDeEjercicio.NumeroDiasMeses = (textNumeroDiasMeses.Text.Length < 0) ? int.Parse(textNumeroDiasMeses.Text):0 ;
            planDeEjercicio.FechaDeEntreno = dateTimeFechaDeEntreno.Value;
            planDeEjercicio.Sesion = comboSesion.Text;
            planDeEjercicio.Objetivo = textObjetivo.Text;
            planDeEjercicio.Estado = comboEstado.Text;
            planDeEjercicio.Ciclo = comboCiclo.Text;
            planDeEjercicio.IdCliente = "#Cl123455";
            planDeEjercicio.GenerarIdPlanEjercicio();
            planDeEjercicio.NombreCliente = "Victor Ardila";
            planDeEjercicio.TipoIdCliente = "CC";
            planDeEjercicio.DescripcionPlanEjercicio = textDescripcionPlan.Text;
            return planDeEjercicio;
        }
        //Valida los datos para calcular
        private void textNumeroDiasMeses_Validated(object sender, EventArgs e)
        {
            CalcularPrecioPlan();
        }
        //Calculo del precio del plan y lo muestro
        private void CalcularPrecioPlan()
        {
            PlanDeEjercicio planDeEjercicio = MapearPlanEjercicio();
            planDeEjercicio.CalcularPrecio();
            labelPrecio.Text = planDeEjercicio.Precio.ToString();
        }
        //<Boton Volver>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //<Boton Buscar>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaClienteRespuesta respuesta = new BusquedaClienteRespuesta();
            string id = textBuscar.Text;
            if (id != "")
            {
                respuesta = clienteService.BuscarPorIdentificacion(id);
                var clientes = new List<Cliente> { respuesta.Cliente };
                dataGridView1.DataSource = clientes;
                if (respuesta.Cliente != null)
                {
                    //Se genera el id del plan automaticamente
                    PlanDeEjercicio planDeEjercicio = MapearPlanEjercicio();
                    planDeEjercicio.GenerarIdPlanEjercicio();
                    labelIdPlanEjercicio.Text = planDeEjercicio.IdPlanDeEjercicio;
                    //Se le setean los valores a plan de ejercicio
                    planDeEjercicio.IdCliente = respuesta.Cliente.Identificacion;
                    planDeEjercicio.TipoIdCliente = respuesta.Cliente.TipoDeIdentificacion;
                    planDeEjercicio.NombreCliente = (respuesta.Cliente.Nombres) + (respuesta.Cliente.Apellidos);
                    //Se habilita el cuadro de datos
                    grupoDatosDeEntrenamiento.Enabled = true;
                }
                else
                {
                    if (respuesta.Cliente == null)
                    {
                        MessageBox.Show(respuesta.Mensaje, "Busqueda por identificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        //<Boton Registrar Plan>
        private void btnRegistrarPlan_Click(object sender, EventArgs e)
        {
            PlanDeEjercicio planDeEjercicio = MapearPlanEjercicio();
            string mensaje = planDeEjercicioService.Guardar(planDeEjercicio);
            MessageBox.Show(mensaje, "Mensaje de Guardado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            Limpiar();
        }
    }
}
