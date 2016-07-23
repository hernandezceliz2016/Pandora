using Business_Logic.Util.autoMaper;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICE;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_SERVICEIMPL;
using Entity_Logic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public void FnInicializarFuncionEnParalelo()
        {
            try
            {
                Func<object> funcion = sendEmail;
                InicializarFuncionEnParalelo(funcion, null);

            }
            catch (Exception ex)
            {
                //ClsFunciones.mensajeAlerta(ex.Message);
            }
        }

        public static async void InicializarFuncionEnParalelo(Func<object> T, Action<object> funcion = null)
        {
            try
            {
                var funcionEnEspera = await EjecutarFuncionEnEspera(T);
                funcion?.Invoke(funcionEnEspera);
            }
            catch (Exception ex)
            {

            }
        }

        private static Task<object> EjecutarFuncionEnEspera(Func<object> funcion)
        {
            return Task.Factory.StartNew(() => funcion());
        }

        public string sendEmail()
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
            return null;

        }
    }
}
