using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Logic.Entity
{
    public class clsUsuario
    {
        public int CodigoUsua { get; set; }
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Nombres { get; set; }
        public string Usua { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> FechExpiracion { get; set; }
        public Nullable<System.DateTime> FechReg { get; set; }
        public Nullable<int> Estado { get; set; }
    }
}
