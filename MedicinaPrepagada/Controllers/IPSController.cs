using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicinaPrepagada.Models.DBMedicinaPrepagada.Source;

namespace MedicinaPrepagada.Controllers
{
    public class IPSController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();

        // GET: IPS        
        public ActionResult Index()
        {
            try
            {
                return View(db.IPS.ToList());
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: IPS/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IPS iPS = db.IPS.Find(id);
                if (iPS == null)
                {
                    return HttpNotFound();
                }
                return View(iPS);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: IPS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IPS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ips,nombre,nit,telefono,nombre_contacto,apellidos_contacto,transferencia,numero_cuenta")] IPS iPS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!iPS.isValidated)
                        throw new Exception();
                    db.IPS.Add(iPS);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(iPS);
            }
            catch (Exception ex)
            {
                foreach (var issue in iPS.GetReglasValidacion())
                    ModelState.AddModelError(issue.propiedad, issue.mensaje);
                return View(iPS);
            }
        }

        // GET: IPS/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IPS iPS = db.IPS.Find(id);
                if (iPS == null)
                {
                    return HttpNotFound();
                }
                return View(iPS);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }            
        }

        // POST: IPS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ips,nombre,nit,telefono,nombre_contacto,apellidos_contacto,transferencia,numero_cuenta")] IPS iPS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!iPS.isValidated)
                        throw new Exception();
                    db.Entry(iPS).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(iPS);
            }
            catch (Exception ex)
            {
                foreach (var issue in iPS.GetReglasValidacion())
                    ModelState.AddModelError(issue.propiedad, issue.mensaje);
                return View(iPS);
            }
        }

        // GET: IPS/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IPS iPS = db.IPS.Find(id);
                if (iPS == null)
                {
                    return HttpNotFound();
                }
                return View(iPS);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: IPS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                IPS iPS = db.IPS.Find(id);
                db.IPS.Remove(iPS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
