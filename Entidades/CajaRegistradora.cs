﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CajaRegistradora
    {
        public CajaRegistradora(string idCaja, DateTime fechaDeApertura, DateTime fechaDeCierre, string estado, double montoInicial, double monto)
        {
            IdCaja = idCaja;
            FechaDeApertura = fechaDeApertura;
            FechaDeCierre = fechaDeCierre;
            Estado = estado;
            Monto = monto;
        }

        public CajaRegistradora()
        {
        }

        public string IdCaja { get; set; }
        public DateTime FechaDeApertura{ get; set; }
        public DateTime FechaDeCierre { get; set; }
        public string Estado { get; set; }
        public double Monto { get; set; }
        //Metodos de la clase
        public void AbrirCaja()
        {
            FechaDeApertura = DateTime.Today;
            FechaDeCierre = DateTime.MinValue;
            Estado = "Abierta";
        }
        public void GenerarIdCaja()
        {
            string a = "#CJ";
            int b;
            string codigo;
            Random aleatorio = new Random();
            b = aleatorio.Next(25000, 30000);
            codigo = a + b;
            IdCaja = codigo;
        }
        public void CerrarCaja()
        {
            FechaDeApertura = DateTime.Today;
            FechaDeCierre = DateTime.Today;
            Estado = "Cerrada";
        }
    }
}
