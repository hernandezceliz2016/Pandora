using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE
{
    public interface IBaseService<G>
    {
        G ObtenerPorId(string Id);
        List<G> Listar();

        object Insertar(G objeto);

        bool Delete(G objeto);

        bool Update(G objeto);

        void IniciarTransacion();

        void FinalizarTransaccionExitosa();

        void FinalizarTransaccionErronea();
    }
}
