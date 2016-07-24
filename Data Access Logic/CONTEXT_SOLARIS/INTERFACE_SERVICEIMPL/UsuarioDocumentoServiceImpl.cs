using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAOIMPL;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL
{
    public class UsuarioDocumentoServiceImpl : IUsuarioDocumentoService
    {

        //IUploadeFileDAO daoFile = new UploadeFileDaoImpl();

        IUsuarioDocumentoDAO db = new UsuarioDocumentoDaoImpl();

        public bool Delete(usuariodocumento objeto)
        {
            throw new NotImplementedException();
        }

        public void FinalizarTransaccionErronea()
        {
            throw new NotImplementedException();
        }

        public void FinalizarTransaccionExitosa()
        {
            throw new NotImplementedException();
        }

        public void IniciarTransacion()
        {
            throw new NotImplementedException();
        }

        public object Insertar(usuariodocumento objeto)
        {
            return db.Insertar(objeto);
        }

        public List<usuariodocumento> Listar()
        {
            return db.Listar();
        }

        public usuariodocumento ObtenerPorId(string Id)
        {
            throw new NotImplementedException();
        }

        public bool Update(usuariodocumento objeto)
        {
            throw new NotImplementedException();
        }
    }
}
