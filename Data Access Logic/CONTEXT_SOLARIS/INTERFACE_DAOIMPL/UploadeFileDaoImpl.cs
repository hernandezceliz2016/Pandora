using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAOIMPL
{
    class UploadeFileDaoImpl : IUploadeFileDAO
    {
        TecnologyContext db = new TecnologyContext();
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
            try
            {
                db.uploadfile.Add(objeto);
                db.SaveChanges();
                return objeto.FileID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
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
