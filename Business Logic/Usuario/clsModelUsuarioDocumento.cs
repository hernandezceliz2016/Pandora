using Business_Logic.Util.autoMaper;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL;
using Entity_Logic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Usuario
{
    public class clsModelUsuarioDocumento
    {
        IUsuarioDocumentoService usuarioDocuService = new UsuarioDocumentoServiceImpl();

        public int FnGuardarArchivo(clsUsuarioDocumento file)
        {
            try
            {
                var entidad = mapear.AutoMapToUsuariodocumento(file);
                var idFile = (int)usuarioDocuService.Insertar(entidad);
                return idFile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
