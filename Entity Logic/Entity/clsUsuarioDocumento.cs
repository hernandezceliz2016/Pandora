using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Logic.Entity
{
    public class clsUsuarioDocumento
    {
        public int CodigoUsuaDoc { get; set; }
        public Nullable<int> CodigoUsua { get; set; }
        public string Descripcion { get; set; }
        public string RutaFisica { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> FechReg { get; set; }
        public Nullable<int> Estado { get; set; }
    }
}
