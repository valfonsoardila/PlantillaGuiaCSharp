using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

//se importa la libreria para arrastrar formulario
using System.Runtime.InteropServices;

namespace YouGym
{
    public partial class YouGym : Form
    {

        int cantidadFormularios = 6;
        public YouGym()
        {
            InitializeComponent();
            Inicializar();
            hideSubMenu();
            logoBackGround.Visible = true;
            //quitra la barra de titulo
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }
        private void Inicializar()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }
        private void CerrarYAbrirImagen()
        {
            if(logoBackGround.Visible == false)
            {
                logo.Visible = true;
            }
            else
            {
                if (logoBackGround.Visible == true)
                {
                    logo.Visible = false;
                }
            }
        }
        //funcion Abrir Formularios
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = PnlContenedorInterno.Controls.OfType<MiForm>().FirstOrDefault();
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                PnlContenedorInterno.Controls.Add(formulario);
                PnlContenedorInterno.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }
        //Funcion cerrar Formularios
        private void CerrarFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = PnlContenedorInterno.Controls.OfType<MiForm>().FirstOrDefault();
            if (formulario != null)
            {
                formulario.Close();
            }
        }
        //Switch para cerrar formularios
        private void CerrarFormularioSwicth(int FormularioCerrar)
        {
            switch (FormularioCerrar)
            {
                case 1:
                    CerrarFormulario<RegistrarDatosUsuario>();
                    break;
                case 2:
                    CerrarFormulario<RegistrarDatosEntrenador>();
                    break;
                case 3:
                    CerrarFormulario<EvolucionesDeUsuario>();
                    break;
                case 4:
                    CerrarFormulario<RegistrarPlanEjercicio>();
                    break;
                case 5:
                    CerrarFormulario<CalculosDeEntrenador>();
                    break;
                case 6:
                    CerrarFormulario<ConsultarDatosEntrenador>();
                    break;
                case 7:
                    CerrarFormulario<ConsultarDatosUsuario>();
                    break;
                case 8:
                    CerrarFormulario<ConsultarDatosPlan>();
                    break;
                case 9:
                    CerrarFormulario<AperturaDeCaja>();
                    break;
                case 10:
                    CerrarFormulario<EliminarDatosPlan>();
                    break;
                case 11:
                    CerrarFormulario<EliminarDatosUsuario>();
                    break;
            }
        }
        //Cerrar los formularios abiertos
        private void CerrarFormulariosCiclo()
        {
            for (int i = 1; i <= cantidadFormularios; i++)
            {
                CerrarFormularioSwicth(i);
            }
        }
        //Funcion para esconder submenu(es) 
        private void hideSubMenu()
        {
            subMenuUsuario.Visible = false;
            subMenuEntrenador.Visible = false;
            subMenuServicios.Visible = false;
            subMenuCaja.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        //Gestionar Usuario
        //<Boton gestionar cliente>
        private void btnGestionarUsuario_Click(object sender, EventArgs e)
        {
            showSubMenu(subMenuUsuario);
        }
        //<Boton registrar cliente>
        private void btnRegistrarDatosUsuario_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarYAbrirImagen();
            CerrarFormulariosCiclo();
            AbrirFormulario<RegistrarDatosUsuario>();
        }
        //<Boton consultar cliente>
        private void btnConsultarDatosUsuario_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<ConsultarDatosUsuario>();
        }
        //<Boton modificar cliente>
        private void btnModificarDatosUsuario_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<ModificarDatosUsuario>();
        }
        //<Boton Eliminar de cliente>
        private void btnEliminarDatosUsuario_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<EliminarDatosUsuario>();
        }
        //<Boton evoluciones de cliente>
        private void btnEvolucionesUsuario_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<EvolucionesDeUsuario>();
        }

        //Gestionar Entrenador
        private void btnGestionarEntrenadores_Click(object sender, EventArgs e)
        {
            showSubMenu(subMenuEntrenador);
        }
        //<Boton registrar entrenador>
        private void btnRegistrarDatosEntrenador_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<RegistrarDatosEntrenador>();
        }
        //<Boton consultar entrenador>
        private void btnConsultarDatosEntrenador_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<ConsultarDatosEntrenador>();
        }
        //<Boton modificar entrenador>
        private void btModificarDatosEntrenador_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<ModificarDatosEntrenador>();
        }
        //<Boton eliminar entrenador>
        private void btnEliminarDatosEntrenador_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<EliminarDatosEntrenador>();
        }
        //<Boton Asignar Plan>
        private void btnAsignarPlanEjercicio_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<AsignarPlanEjercicio>();
        }
        //Gestionar Servicios
        private void btnGestionarServicios_Click_1(object sender, EventArgs e)
        {
            showSubMenu(subMenuServicios);
        }
        private void btnRegistrarServicios_Click_1(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<RegistrarPlanEjercicio>();
        }
        private void btnConsultarServicios_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<ConsultarDatosPlan>();
        }
        private void btnEliminarDatosServicio_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<EliminarDatosPlan>();
        }
        private void EliminarUsuarioEntrenador_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<EliminarDatosUsuario>();
        }
        //Gestionar caja
        private void btnGestionarCaja_Click(object sender, EventArgs e)
        {
            showSubMenu(subMenuCaja);
        }
        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<AperturaDeCaja>();
        }
        private void btnCierreDeCaja_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            AbrirFormulario<CierreDeCaja>();
        }
        //Cerrar Sesion
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            logoBackGround.Visible = false;
            CerrarFormulariosCiclo();
            this.Close();
        }
        //Botones de control
        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Maximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Maximizar.Visible = false;
            Restaurar.Visible = true;
        }
        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void Restaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Restaurar.Visible = false;
            Maximizar.Visible = true;
        }
        //Arrastrar formulario
        private void menuTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void PnlContenedorInterno_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
    }
}