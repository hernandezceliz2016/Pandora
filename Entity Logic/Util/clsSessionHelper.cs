using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entity_Logic.Entity;

namespace Entity_Logic.Util
{
    public class clsSessionHelper
    {
        /// <summary>
        /// NOMBRE DE LAS SESIONES PARA LUEGO SER LLAMADAS
        /// </summary>
        private const string SESSION_USUARIO = "sessionUsuario";


        private static T FnObtenerSession<T>(string variable)
        {
            var objValor = HttpContext.Current.Session[variable];
            if (objValor == null)
            {
                return default(T);
            }
            return (T)objValor;
        }

        private static void FnSetearSession(string variable, object valor)
        {
            HttpContext.Current.Session[variable] = valor;
        }

        public static clsUsuario FnGetUserSession
        {
            get { return FnObtenerSession<clsUsuario>(SESSION_USUARIO); }
            set { FnSetearSession(SESSION_USUARIO, value); }
        }

        public static bool SessionExpirada => FnGetUserSession == null || FnGetUserSession?.CodigoUsua == 0;
    }
}
