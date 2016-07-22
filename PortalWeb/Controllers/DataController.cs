using Entity_Logic.Util;
using System;
using System.IO;
using System.Web.Mvc;
using Business_Logic.Usuario;
using Entity_Logic.Entity;

namespace PortalWeb.Controllers
{
    public class DataController : Controller
    {
        //clsModelUploadFile lnFile = new clsModelUploadFile();
        clsModelUsuarioDocumento objUsuaDocLn = new clsModelUsuarioDocumento();
        // GET: Data
        public JsonResult SaveFiles(string description)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;

            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("/UploadedFiles"), fileName));
                    clsUsuarioDocumento f = new clsUsuarioDocumento
                    {
                        CodigoUsua = clsSessionHelper.FnGetUserSession.CodigoUsua,
                        Descripcion = description,
                        RutaFisica = fileName,
                        FechReg = DateTime.Now,
                        Estado = 1,
                    };
                    {
                        var codigo = objUsuaDocLn.FnGuardarArchivo(f);
                        if (codigo > 0)
                        {
                            Message = "File uploaded successfully";
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message = "File upload fail! Pleade try again";
                }
            }
            return new JsonResult { Data = new { Message = Message, Status = flag } };
        }
    }
}
