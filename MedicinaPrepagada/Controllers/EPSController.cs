using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicinaPrepagada.Models.DBMedicinaPrepagada.Source;
using MedicinaPrepagada.Models.DBMedicinaPrepagada.Repository;

namespace MedicinaPrepagada.Controllers
{
    public class EPSController : Controller
    {
        private RepositoryEPS db = new RepositoryEPS();

        // GET: EPS
        public ActionResult Index()
        {
            return View(db.listar());
        }

        // GET: EPS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPS ePS = db.buscarPorID(id.Value);
            if (ePS == null)
            {
                return HttpNotFound();
            }
            return View(ePS);
        }

        // GET: EPS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EPS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,nit,telefono,nombre_contacto,apellidos_contacto,transferencia,numero_cuenta")] EPS ePS)
        {
            if (ModelState.IsValid)
            {
                db.agregar(ePS);
                db.guardar();
                return RedirectToAction("Index");
            }

            return View(ePS);
        }

        // GET: EPS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPS ePS = db.buscarPorID(id.Value);
            if (ePS == null)
            {
                return HttpNotFound();
            }
            return View(ePS);
        }

        // POST: EPS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_eps,nombre,nit,telefono,nombre_contacto,apellidos_contacto,transferencia,numero_cuenta")] EPS ePS)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(ePS).State = EntityState.Modified;
                //db.SaveChanges();
                db.editar(ePS);
                db.guardar();
                return RedirectToAction("Index");
            }
            return View(ePS);
        }

        // GET: EPS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPS ePS = db.buscarPorID(id.Value);
            if (ePS == null)
            {
                return HttpNotFound();
            }
            return View(ePS);
        }

        // POST: EPS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EPS ePS = db.buscarPorID(id);
            db.eliminar(ePS);
            db.guardar();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
