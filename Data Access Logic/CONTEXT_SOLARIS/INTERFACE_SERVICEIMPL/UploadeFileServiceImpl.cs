using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAOIMPL;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL
{
    public class UploadeFileServiceImpl : IUploadeFileService
    {
        IUploadeFileDAO daoFile = new UploadeFileDaoImpl();
        public uploadfile ObtenerPorId(string Id)
        {
            throw new NotImplementedException();
        }

        public List<uploadfile> Listar()
        {
            throw new NotImplementedException();
        }

        public object Insertar(uploadfile objeto)
        {
            return daoFile.Insertar(objeto);
        }

        public bool Delete(uploadfile objeto)
        {
            throw new NotImplementedException();
        }

        public bool Update(uploadfile objeto)
        {
            throw new NotImplementedException();
        }

        public void IniciarTransacion()
        {
            throw new NotImplementedException();
        }

        public void FinalizarTransaccionExitosa()
        {
            throw new NotImplementedException();
        }

        public void FinalizarTransaccionErronea()
        {
            throw new NotImplementedException();
        }
    }
}
