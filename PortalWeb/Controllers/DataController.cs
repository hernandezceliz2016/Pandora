using Entity_Logic.Util;
using System;
using System.IO;
using System.Web.Mvc;
using Business_Logic.Usuario;
using Entity_Logic.Entity;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Collections;

namespace PortalWeb.Controllers
{
    public class DataController : Controller
    {

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
                            //var objTemporal = SendMailAsync();
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

        public async Task<ActionResult> SendMailAsync()
        {
            Func<ActionResult> t = sendEmail;
            ActionResult funcionEnEspera = await EjecutarFuncionEnEspera(t);           
            return View();
        }            
      

        private static Task<ActionResult> EjecutarFuncionEnEspera(Func<ActionResult> funcion)
        {
            return Task.Factory.StartNew(() => funcion());
        }

        public ActionResult sendEmail()
        {

            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("garcia.conza@gmail.com"));
            email.From = new MailAddress("garcia.conza@gmail.com");
            email.Subject = "[NOTIFICACION] ADJUNTARON ARCHIVO";
            email.Body = "Se le informa que el cliente Christian Garcia acaba de adjuntra archivo.";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;


            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("garcia.conza@gmail.com", "28JSF28.");

                string output = null;

                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    output = "Corre electrónico fue enviado satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    output = "Error enviando correo electrónico: " + ex.Message;
                }

            }
            return View();
        }

    }
}
