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
    public class TitularesController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();

        // GET: Titulares
        public ActionResult Index()
        {
            var titulares = db.Titulares.Include(t => t.Membresias).Include(t => t.Pacientes);
            return View(titulares.ToList());
        }

        // GET: Titulares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titulares titulares = db.Titulares.Find(id);
            if (titulares == null)
            {
                return HttpNotFound();
            }
            return View(titulares);
        }

        // GET: Titulares/Create
        public ActionResult Create()
        {
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion");
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre");
            return View();
        }

        // POST: Titulares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_titular,id_paciente,id_membresia,habilitado,fecha_registro")] Titulares titulares)
        {
            if (ModelState.IsValid)
            {
                db.Titulares.Add(titulares);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", titulares.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", titulares.id_paciente);
            return View(titulares);
        }

        // GET: Titulares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titulares titulares = db.Titulares.Find(id);
            if (titulares == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", titulares.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", titulares.id_paciente);
            return View(titulares);
        }

        // POST: Titulares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_titular,id_paciente,id_membresia,habilitado,fecha_registro")] Titulares titulares)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titulares).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", titulares.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", titulares.id_paciente);
            return View(titulares);
        }

        // GET: Titulares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titulares titulares = db.Titulares.Find(id);
            if (titulares == null)
            {
                return HttpNotFound();
            }
            return View(titulares);
        }

        // POST: Titulares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Titulares titulares = db.Titulares.Find(id);
            db.Titulares.Remove(titulares);
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
