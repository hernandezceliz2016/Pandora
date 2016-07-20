using System;
using System.Web.Mvc;
using Business_Logic.Usuario;
using Entity_Logic.Entity;
using Entity_Logic.Util;

namespace PortalWeb.Controllers
{
    public class UserController : Controller
    {
        readonly clsModelUsuario lnUser = new clsModelUsuario();

        #region Funciones CRUD
        // POST: User/Create
        [HttpPost]
        public JsonResult Guardar(clsUsuario user)
        {
            string strMensaje;
            var intResp = 0;
            try
            {
                strMensaje = FnValidarRegistroUser(user);
                if (strMensaje.Equals(string.Empty))
                {
                    user.Estado = 1;
                    user.FechReg = DateTime.Now;
                    intResp = lnUser.FnRegistrarUsuario(user);
                    strMensaje = intResp > 0 ? "Se ha Registrado Correctamente" : "Error al Registrar el Usuario";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                strMensaje = "Error en registrar Comuniquese con el Administrador";
            }
            return Json(new { Data = strMensaje, Status = intResp }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Funciones Publicas

        public ActionResult Index()
        {
            if (FnEstaAutentificado())
            {
                switch (clsContantes.userEstado)
                {
                    case 0:
                        return Caducado();
                    case 1:
                        // enviar pagina de gugo
                        break;
                    case 2:
                        return Registrar();
                }
            }
            return View();
        }

        public ActionResult Registrar()
        {
            if (FnEstaAutentificado())
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Caducado()
        {
            return View();
        }

        public ActionResult Login(clsUsuario user)
        {
            try
            {
                var strMensaje = FnValidarUserLogin(user);
                if (string.Empty.Equals(strMensaje))
                {
                    var entidad = lnUser.FnObtenerUsuarioPorLogin(user);
                    if (entidad.CodigoUsua > 0)
                    {
                        clsContantes.userApellido = entidad.Apellido;
                        clsContantes.userCodigo = entidad.CodigoUsua;
                        clsContantes.userDni = entidad.Dni;
                        clsContantes.userNombre = entidad.Nombre;
                        clsContantes.userEstado = (int)entidad.Estado;
                        switch (entidad.Estado)
                        {
                            case 0:
                                return Caducado();
                            case 1:
                                var redirectUrl1 = new UrlHelper(Request.RequestContext).Action("Part8", "Home");
                                return Json(new { Url = redirectUrl1 });
                            case 2:
                                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Registrar", "User");
                                return Json(new { Url = redirectUrl });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { Url = new UrlHelper(Request.RequestContext).Action("Caducado", "User") });
        }

        #endregion

        #region Funciones de Validaciones

        private string FnValidarRegistroUser(clsUsuario user)
        {
            try
            {
                var strMensaje = string.Empty;
                strMensaje = lnUser.FnValidarDisponibilidadEmailNew(user.Email) ? strMensaje
                    : strMensaje + "El Email ya se encuentra registrado \n";
                strMensaje = lnUser.FnValidarUserNew(user.Usua) ? strMensaje
                    : strMensaje + "El Usuario ya se encuentra registrado \n";
                strMensaje = lnUser.FnValidarDisponibilidadDniNew(user.Dni) ? strMensaje
                    : strMensaje + "El Dni ya se encuentra registrado \n";
                strMensaje = !string.IsNullOrEmpty(user.Dni) ? strMensaje
                   : strMensaje + "Ingrese el N° de DNI \n";
                strMensaje = !string.IsNullOrEmpty(user.Email) ? strMensaje
                  : "Ingrese el Email \n";
                strMensaje = !string.IsNullOrEmpty(user.Nombre) ? strMensaje
                  : "Ingrese el Nombre \n";
                strMensaje = !string.IsNullOrEmpty(user.Apellido) ? strMensaje
                  : "Ingrese el Apellido \n";
                return strMensaje;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Error al Validar";
        }

        private string FnValidarUserLogin(clsUsuario user)
        {
            try
            {
                var strMensaje = string.Empty;
                strMensaje = string.IsNullOrEmpty(user.Usua) ? strMensaje + "Ingrese el Usuario \n" : strMensaje;
                strMensaje = user.Usua.Length > 15 ? strMensaje + "El usuario  contiene Maximo 15 caracteres \n" : strMensaje;
                strMensaje = string.IsNullOrEmpty(user.Pass) ? strMensaje + "Ingrese la Password \n" : strMensaje;
                return strMensaje;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Error al Validar";
        }

        private bool FnEstaAutentificado()
        {
            try
            {
                var blnResp = false;
                blnResp = clsContantes.userCodigo > 0 && !string.IsNullOrEmpty(clsContantes.userApellido) &&
                    !string.IsNullOrEmpty(clsContantes.userDni);
                return blnResp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        #endregion

        #region Funciones del MVC

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Index();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return Index();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Index();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return Index();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Index();
            }
        }

        #endregion
    }

}
