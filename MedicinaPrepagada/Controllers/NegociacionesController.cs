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
    public class NegociacionesController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();

        // GET: Negociaciones
        public ActionResult Index()
        {
            var negociaciones = db.Negociaciones.Include(n => n.IPS).Include(n => n.Servicios);
            return View(negociaciones.ToList());
        }

        // GET: Negociaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Negociaciones negociaciones = db.Negociaciones.Find(id);
            if (negociaciones == null)
            {
                return HttpNotFound();
            }
            return View(negociaciones);
        }

        // GET: Negociaciones/Create
        public ActionResult Create()
        {
            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre");
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion");
            return View();
        }

        // POST: Negociaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_negociacion,id_servicio,id_ips,valor,habilitado")] Negociaciones negociaciones)
        {
            if (ModelState.IsValid)
            {
                db.Negociaciones.Add(negociaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre", negociaciones.id_ips);
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", negociaciones.id_servicio);
            return View(negociaciones);
        }

        // GET: Negociaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Negociaciones negociaciones = db.Negociaciones.Find(id);
            if (negociaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre", negociaciones.id_ips);
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", negociaciones.id_servicio);
            return View(negociaciones);
        }

        // POST: Negociaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_negociacion,id_servicio,id_ips,valor,habilitado")] Negociaciones negociaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(negociaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre", negociaciones.id_ips);
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", negociaciones.id_servicio);
            return View(negociaciones);
        }

        // GET: Negociaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Negociaciones negociaciones = db.Negociaciones.Find(id);
            if (negociaciones == null)
            {
                return HttpNotFound();
            }
            return View(negociaciones);
        }

        // POST: Negociaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Negociaciones negociaciones = db.Negociaciones.Find(id);
            db.Negociaciones.Remove(negociaciones);
            db.SaveChanges();
            return RedirectToAction("Index");
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
