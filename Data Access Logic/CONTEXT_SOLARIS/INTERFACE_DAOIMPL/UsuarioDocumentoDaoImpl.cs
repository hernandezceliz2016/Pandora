using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAOIMPL
{
    class UsuarioDocumentoDaoImpl : IUsuarioDocumentoDAO
    {
        TecnologyContext db = new TecnologyContext();

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
            try
            {
                db.usuariodocumento.Add(objeto);
                db.SaveChanges();
                var id = db.usuariodocumento.Max(u => u.CodigoUsuaDoc);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public List<usuariodocumento> Listar()
        {
            var usuariosDocumentos = db.usuariodocumento;

            return usuariosDocumentos.ToList();
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
