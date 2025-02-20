﻿using System;
using System.Web;
using System.Web.Mvc;
using Business_Logic.Usuario;
using Entity_Logic.Entity;
using Entity_Logic.Util;

namespace PortalWeb.Controllers
{
    public class UserController : Controller
    {
        readonly clsModelUsuario lnUser = new clsModelUsuario();
        private readonly string strSessionMensaje = string.Empty;

        public UserController()
        {
            if (clsSessionHelper.SessionExpirada)
            {
                strSessionMensaje = "LA SESSION HA EXPIRADO";
            }
        }

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

        public JsonResult BuscarPorDni(string strDni)
        {
            try
            {
                var entUser = lnUser.FnBuscarUsuarioPorDni(strDni);
                if (entUser?.CodigoUsua > 0)
                {
                    return Json(new { Estado = clsContantes.estado.Succes, Data = entUser, strMensaje = string.Empty }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Estado = clsContantes.estado.Failured, Data = string.Empty, strMensaje = clsContantes.mensajeBusqNoEncontrada }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { Estado = clsContantes.estado.ErrorCritico, Data = string.Empty, strMensaje = clsContantes.mensajeErrorCritico },
                JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Funciones Publicas

        public ActionResult Index()
        {
            if (!clsSessionHelper.SessionExpirada)
            {
                switch (clsSessionHelper.FnGetUserSession.Estado)
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
            if (!clsSessionHelper.SessionExpirada)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Modificar(clsUsuario user)
        {
            try
            {
                var strMensaje = FnValidarModificacionUser(user);
                if (strMensaje.Equals(string.Empty))
                {
                    user.Estado = 1;
                    user.CodigoUsua = lnUser.FnBuscarUsuarioPorDni(user.Dni).CodigoUsua;
                    var blnResp = new clsModelUsuario().FnModificarUsuario(user);
                    return Json(blnResp ? new { Estado = clsContantes.estado.Succes, strMensaje = clsContantes.mensajeGuarExito } : new { Estado = clsContantes.estado.Failured, strMensaje = clsContantes.mensajeGuarError });
                }
                return Json(new { Estado = clsContantes.estado.Failured, strMensaje });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { Estado = clsContantes.estado.ErrorCritico, strMensaje = clsContantes.mensajeErrorCritico });
        }

        // GET: User/Edit/5
        public ActionResult Modificar()
        {
            if (!clsSessionHelper.SessionExpirada)
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
                    clsSessionHelper.FnGetUserSession = entidad;
                    if (clsSessionHelper.FnGetUserSession?.CodigoUsua > 0)
                    {
                        switch (clsSessionHelper.FnGetUserSession.Estado)
                        {
                            case 0:
                                return Caducado();
                            case 1:
                                var redirectUrl = new UrlHelper(Request.RequestContext).Action("UpLoad", "Home");
                                return Json(new { Estado = clsContantes.estado.Succes, Url = redirectUrl, strMensaje = clsContantes.mensajeLoginExito });
                            case 2:
                                var Usuario = clsSessionHelper.FnGetUserSession.Usua;
                                var redirectUrl2 = new UrlHelper(Request.RequestContext).Action("Registrar", "User");
                                return Json(new { Estado = clsContantes.estado.Succes, Url = redirectUrl2, strMensaje = clsContantes.mensajeLoginExito, User = Usuario });
                        }
                    }
                    return Json((new { Estado = clsContantes.estado.Failured, Url = new UrlHelper(Request.RequestContext).Action("Index", "User"), strMensaje = clsContantes.mensajeLoginError }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(new { Estado = clsContantes.estado.ErrorCritico, Url = new UrlHelper(Request.RequestContext).Action("Caducado", "User") });
        }

        public ActionResult CerrarSession()
        {
            try
            {
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie(clsSessionHelper.ObtenerSessionUser));
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index", "User");
        }

        public JsonResult ObtenerUserLogin()
        {
            if (!clsSessionHelper.SessionExpirada)
            {
                var User = clsSessionHelper.FnGetUserSession;
                return Json(new { Estado = clsContantes.estado.Succes, User }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Estado = clsContantes.estado.Desctivado }, JsonRequestBehavior.AllowGet);
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

        private string FnValidarModificacionUser(clsUsuario user)
        {
            try
            {
                var strMensaje = string.Empty;
                strMensaje = lnUser.FnValidarEmailCambio(user.Email.Trim(), user.Dni) ? strMensaje
                   : strMensaje + "El Email ya se encuentra registrado \n";
                strMensaje = lnUser.FnValidarUserCambio(user.Usua.Trim(), user.Dni) ? strMensaje
                    : strMensaje + "El Usuario ya se encuentra registrado \n";
                //strMensaje = lnUser.FnValidarDisponibilidadDniNew(user.Dni) ? strMensaje
                //    : strMensaje + "El Dni ya se encuentra registrado \n";
                //strMensaje = !string.IsNullOrEmpty(user.Dni) ? strMensaje
                //   : strMensaje + "Ingrese el N° de DNI \n";
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
            return clsContantes.mensajeErrorCritico;
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
