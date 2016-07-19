using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO
{
    interface IUsuarioDAO : IBaseDAO<usuario>
    {
        
        int ObtenerCorrelativo();

        usuario ObtenerPorUserPass(usuario entidad);

        bool UserDisponible(string strUser);

        bool EmailDisponible(string strEmail);

        bool DniDisponible(string strDni);

        usuario ObtenerPorDni(string strDni);
    }
}
