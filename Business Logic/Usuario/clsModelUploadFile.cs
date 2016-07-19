using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Logic.Util.autoMaper;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL;
using Entity_Logic.Util;

namespace Business_Logic.Usuario
{
    public class clsModelUploadFile
    {
        IUploadeFileService uploadService = new UploadeFileServiceImpl();

        public int FnGuardarArchivo(UploadedFile file)
        {
            try
            {
                var entidad = mapear.AutoMapTouploadfile(file);
                var idFile = (int)uploadService.Insertar(entidad);
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
