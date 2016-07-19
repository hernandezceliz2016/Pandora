using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Util.autoMaper
{
    public class clsServicioBase
    {
        protected clsServicioBase()
        {
            clsAutoMaper.InicializarAutoMaper();
        }
    }
}
