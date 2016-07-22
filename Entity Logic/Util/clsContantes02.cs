using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Logic.Util
{
    public static partial class clsContantes
    {
        //public static string userNombre;

        //public static string userApellido;

        //public static int userCodigo;

        //public static string userDni;

        //public static int userEstado;


       /* 
            INICIO DE MENSAJES DEL SISTEMA             
       */
        public static string mensajeGuarExito = "SE HA REGISTRADO CORRECTAMENTE";
        public static string mensajeGuarError = "NO SE HA PODIDO GUARDAR";
        public static string mensajeLoginExito = "ACCESO CONCEDIDO";
        public static string mensajeLoginError = "USUARIO NO REGISTRADO";
        public static string mensajeErrorCritico = "SE HA PRODUCIDO UN ERROR CRITICO";


        public enum estado
        {
            Acitvo = 1,
            Desctivado = 0,
            Succes = 1,
            Failured = 0,
            ErrorCritico = 2
        }

    }
}
