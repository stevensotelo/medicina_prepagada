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
    public class OrdenesController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();
                

        // GET: Ordenes/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                ViewBag.id = id == null ? "" : id.Value.ToString();
                Ordenes ordenes = null;
                if (id == null || (ordenes = db.Ordenes.Find(id)) == null)
                {
                    return View(ordenes);
                }
                return View();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }            
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre");
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion");
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre");
            ViewBag.id_servicio = new SelectList(db.Servicios.Where(p=>p.habilitado), "id_servicio", "descripcion");
            return View();
        }

        // POST: Ordenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden,id_paciente,id_servicio,id_ips,id_membresia,fecha_solicitud,valor_copago,valor_cuota_moderadora,estado")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {
                ordenes.estado = "pendiente";
                ordenes.fecha_solicitud = DateTime.Now;
                ordenes.valor_copago = 0;
                ordenes.valor_cuota_moderadora = 0;
                db.Ordenes.Add(ordenes);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = ordenes.id_orden });
            }

            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre", ordenes.id_ips);
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", ordenes.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", ordenes.id_paciente);
            ViewBag.id_servicio = new SelectList(db.Servicios.Where(p => p.habilitado), "id_servicio", "descripcion", ordenes.id_servicio);
            return View(ordenes);
        }

        // GET: Ordenes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.id = id == null ? "" : id.Value.ToString();
            Ordenes ordenes = null;
            if (id == null || (ordenes = db.Ordenes.Find(id)) == null)
            {
                return View(ordenes);
            }
            return View();
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_orden,id_paciente,id_servicio,id_ips,id_membresia,fecha_solicitud,valor_copago,valor_cuota_moderadora,estado")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {   
                var negociacion = db.Negociaciones.SingleOrDefault(p => p.id_ips == ordenes.id_ips && p.id_servicio == p.id_servicio);

                db.Entry(ordenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre", ordenes.id_ips);
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion", ordenes.id_membresia);
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre", ordenes.id_paciente);
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", ordenes.id_servicio);
            return View(ordenes);
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
