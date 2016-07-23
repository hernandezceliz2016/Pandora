using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAO;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;


namespace Data_Access_Logic.CONTEXT_SOLARIS.INTERFACE_DAOIMPL
{
    class UsuarioDaoImpl : IUsuarioDAO
    {
        private TecnologyContext db = new TecnologyContext();

        private DbContextTransaction objTransacion { get; set; }

        public usuario ObtenerPorId(string Id)
        {
            throw new NotImplementedException();
        }

        public usuario ObtenerPorDni(string strDni)
        {
            try
            {
                var user = db.usuario.FirstOrDefault(mc => mc.Dni.Equals(strDni));
                return user;
            }
            catch (Exception ex)
            {
            }
            return new usuario();
        }

        public List<usuario> Listar()
        {
            throw new NotImplementedException();
        }

        public usuario ObtenerPorUserPass(usuario entidad)
        {
            var user = new usuario();
            try
            {
                user = db.usuario.FirstOrDefault(mc => mc.Usua.Equals(entidad.Usua)
               && mc.Pass.Equals(entidad.Pass));
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public bool UserDisponible(string strUser)
        {
            try
            {
                var disponible = !db.usuario.Any(mc => mc.Usua.Equals(strUser));
                return disponible;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool EmailDisponible(string strEmail)
        {
            try
            {
                var disponible = db.usuario.Where(mc => mc.Email.Equals(strEmail)).Count() == 0;
                return disponible;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool DniDisponible(string strDni)
        {
            try
            {
                var disponible = db.usuario.Where(mc => mc.Dni.Equals(strDni)).Count() == 0;
                return disponible;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public object Insertar(usuario objeto)
        {
            try
            {
                db.usuario.Add(objeto);
                db.SaveChanges();
                return objeto.CodigoUsua;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                //  throw;
            }
            return 0;
        }

        public int ObtenerCorrelativo()
        {
            try
            {
                var lngCorrelativo = (db.usuario.Max(x => (int?)x.CodigoUsua) ?? 0) + 1;
                return lngCorrelativo;
            }
            catch (Exception ex)
            {
                // Logica para controlar Errors implementar
                // throw;
            }
            return 0;
        }

        public bool Delete(usuario objeto)
        {
            throw new NotImplementedException();
        }

        public bool Update(usuario objeto)
        {
            try
            {
                db.Entry(objeto).State = EntityState.Modified;
                // db.Entry(objeto).Property(mc => mc.CodigoUsua).IsModified = false;
                db.Entry(objeto).Property(mc => mc.FechReg).IsModified = false;
                db.Entry(objeto).Property(mc => mc.Dni).IsModified = false;
                var blnResp = db.SaveChanges() > 0;
                return blnResp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public void IniciarTransacion()
        {
            objTransacion = db.Database.BeginTransaction();
        }

        public void FinalizarTransaccionExitosa()
        {
            objTransacion.Commit();

        }

        public void FinalizarTransaccionErronea()
        {
            objTransacion.Rollback();
        }
    }
}
