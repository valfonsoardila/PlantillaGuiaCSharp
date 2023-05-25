﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Logica
{
    public class IdUsuarioTxtConsultaResponse
    {
        public List<IdUsuarioTxt> IdEmpleadoTxts { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }
        public bool Encontrado { get; set; }

        public IdUsuarioTxtConsultaResponse(string mensaje)
        {
            Mensaje = mensaje;
            Encontrado = false;
        }
        public IdUsuarioTxtConsultaResponse(List<IdUsuarioTxt> idEmpleadoTxts)
        {
            IdEmpleadoTxts = new List<IdUsuarioTxt>();
            IdEmpleadoTxts = idEmpleadoTxts;
            Encontrado = true;
        }
    }
}
