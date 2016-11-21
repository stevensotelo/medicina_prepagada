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
    public class BeneficiariosController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();

        // GET: Beneficiarios
        public ActionResult Index()
        {
            var beneficiarios = db.Beneficiarios.Include(b => b.Membresias).Include(b => b.Pacientes).Include(b => b.Titulares);
            return View(beneficiarios.ToList());
        }

        // GET: Beneficiarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiarios beneficiarios = db.Beneficiarios.Find(id);
            if (beneficiarios == null)
            {
                return HttpNotFound();
            }
            return View(beneficiarios);
        }

        // GET: Beneficiarios/Create
        public ActionResult Create()
        {
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion");
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre");
            ViewBag.id_titular = new SelectList(db.Titulares, "id_titular", "id_titular");
            return View();
        }

        // POST: Beneficiarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_beneficiario,id_titular,id_paciente,id_membresia,habilitado,fecha_registro")] Beneficiarios beneficiarios)
        {
            if (ModelState.IsValid)
            {
                db.Beneficiarios.Add(beneficiarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", beneficiarios.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", beneficiarios.id_paciente);
            ViewBag.id_titular = new SelectList(db.Titulares, "id_titular", "id_titular", beneficiarios.id_titular);
            return View(beneficiarios);
        }

        // GET: Beneficiarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiarios beneficiarios = db.Beneficiarios.Find(id);
            if (beneficiarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", beneficiarios.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", beneficiarios.id_paciente);
            ViewBag.id_titular = new SelectList(db.Titulares, "id_titular", "id_titular", beneficiarios.id_titular);
            return View(beneficiarios);
        }

        // POST: Beneficiarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_beneficiario,id_titular,id_paciente,id_membresia,habilitado,fecha_registro")] Beneficiarios beneficiarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", beneficiarios.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", beneficiarios.id_paciente);
            ViewBag.id_titular = new SelectList(db.Titulares, "id_titular", "id_titular", beneficiarios.id_titular);
            return View(beneficiarios);
        }

        // GET: Beneficiarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiarios beneficiarios = db.Beneficiarios.Find(id);
            if (beneficiarios == null)
            {
                return HttpNotFound();
            }
            return View(beneficiarios);
        }

        // POST: Beneficiarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beneficiarios beneficiarios = db.Beneficiarios.Find(id);
            db.Beneficiarios.Remove(beneficiarios);
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
