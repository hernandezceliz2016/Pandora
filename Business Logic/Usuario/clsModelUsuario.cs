using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Logic.Util.autoMaper;
using Business_Logic.Util.funciones;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL;
using Entity_Logic.Entity;

namespace Business_Logic.Usuario
{
    public class clsModelUsuario : clsServicioBase
    {
        private IUsuarioService daoUsuario = new UsuarioServiceImpl();

        public int FnRegistrarUsuario(clsUsuario usuario)
        {
            try
            {
                daoUsuario.IniciarTransacion();
                var codigoUsuario = daoUsuario.ObtenerCorrelativo();
                usuario.CodigoUsua = codigoUsuario;
                var codigo = (int)daoUsuario.Insertar(mapear.AutoMapToUsuario(usuario));
                var blnResp = codigo > 0;
                if (blnResp)
                {
                    daoUsuario.FinalizarTransaccionExitosa();
                }
                else
                {
                    daoUsuario.FinalizarTransaccionErronea();
                }
                return codigo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool FnModificarUsuario(clsUsuario usuario)
        {
            try
            {
                var entidad = mapear.AutoMapToUsuario(usuario);
                var blnResp = daoUsuario.Update(entidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public clsUsuario FnObtenerUsuarioPorLogin(clsUsuario usuario)
        {
            try
            {
                var user = daoUsuario.ObtenerPorUserPass(mapear.AutoMapToUsuario(usuario));
                return mapear.AutoMapToClsUsuario(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new clsUsuario();
        }

        public bool FnValidarUserNew(string strUser)
        {
            try
            {
                return daoUsuario.UserDisponible(strUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool FnValidarDisponibilidadEmailNew(string strEmail)
        {
            try
            {
                return daoUsuario.EmailDisponible(strEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool FnValidarDisponibilidadDniNew(string strDni)
        {
            try
            {
                return daoUsuario.DniDisponible(strDni);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public clsUsuario FnBuscarUsuarioPorDni(string strDni)
        {
            try
            {
                var entidad = daoUsuario.ObtenerPorDni(strDni);
                return mapear.AutoMapToClsUsuario(entidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new clsUsuario();
        }
    }
}
