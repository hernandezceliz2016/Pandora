using Entity_Logic.Entity;
using Entity_Logic.Entity.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE
{
    public interface IUsuarioDocumentoService : IBaseService<usuariodocumento>
    {
        List<clsGetAllFile> GetListAllFile();
    }
}
