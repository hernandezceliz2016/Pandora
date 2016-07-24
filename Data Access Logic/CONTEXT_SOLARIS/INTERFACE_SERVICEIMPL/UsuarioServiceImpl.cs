using System;
using System.Collections.Generic;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAOIMPL;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL
{
    public class UsuarioServiceImpl : IUsuarioService
    {
        IUsuarioDAO daoUsuario = new UsuarioDaoImpl();
        public usuario ObtenerPorId(string Id)
        {
            throw new NotImplementedException();
        }

        public List<usuario> Listar()
        {
            throw new NotImplementedException();
        }

        public object Insertar(usuario objeto)
        {
            return daoUsuario.Insertar(objeto);
        }

        public bool Delete(usuario objeto)
        {
            throw new NotImplementedException();
        }

        public bool Update(usuario objeto)
        {
            return daoUsuario.Update(objeto);
        }

        public void IniciarTransacion()
        {
            daoUsuario.IniciarTransacion();
        }

        public void FinalizarTransaccionExitosa()
        {
            daoUsuario.FinalizarTransaccionExitosa();
        }

        public void FinalizarTransaccionErronea()
        {
            daoUsuario.FinalizarTransaccionErronea();
        }

        public int ObtenerCorrelativo()
        {
            return daoUsuario.ObtenerCorrelativo();
        }

        public usuario ObtenerPorUserPass(usuario entidad)
        {
            return daoUsuario.ObtenerPorUserPass(entidad);
        }

        public bool UserDisponible(string strUser)
        {
            return daoUsuario.UserDisponible(strUser);
        }

        public bool EmailDisponible(string strEmail)
        {
            return daoUsuario.EmailDisponible(strEmail);
        }

        public bool DniDisponible(string strDni)
        {
            return daoUsuario.DniDisponible(strDni);
        }

        public usuario ObtenerPorDni(string strDni)
        {
            return daoUsuario.ObtenerPorDni(strDni);
        }
    }
}
