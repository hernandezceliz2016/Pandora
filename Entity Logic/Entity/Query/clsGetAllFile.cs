using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Logic.Entity.Query
{
    public class clsGetAllFile
    {
        public int CodigoUsuaDoc { get; set; }
        public string Nombre { get; set; }
        public string RutaFisica { get; set; }
        public Nullable<System.DateTime> FechReg { get; set; }
        public Nullable<int> Estado { get; set; }
    }
}
