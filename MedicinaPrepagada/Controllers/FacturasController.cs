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
    public class FacturasController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();

        // GET: Facturas
        public ActionResult Index(string fecha)
        {
            DateTime inicio = DateTime.Parse(fecha);
            var facturas = db.Facturas.Where(p => p.fecha_factura_inicio == inicio);
            return View(facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            double total = facturas.valor_a_pagar;
            foreach (var b in facturas.Facturas_Beneficiarios)
                total += b.valor_a_pagar;
            ViewBag.total = total;
            return View(facturas);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            int ano = int.Parse(form["ano"].ToString());
            int mes = int.Parse(form["mes"].ToString());
            DateTime inicio = new DateTime(ano, mes, 1);
            DateTime fin = new DateTime(ano, mes + 1, 1);
            try
            {
                var titulares = from t in db.Titulares.Where(p => p.fecha_registro >= inicio && p.fecha_registro <= fin)
                                join f in db.Facturas on t.id_titular equals f.id_titular
                                where f.fecha_factura_inicio != inicio
                                select t;
                DBPrepagadaEntities db2 = new DBPrepagadaEntities();
                using (db2)
                {
                    foreach (var titular in titulares)
                    {
                        Facturas factura = new Facturas() { estado = "pendiente", fecha_factura = DateTime.Now.Date, fecha_factura_inicio = inicio, fecha_factura_fin = fin, valor_a_pagar = titular.Membresias.valor_mensual, id_titular = titular.id_titular, fecha_cancelado = inicio, pagado = 0 };
                        db2.Facturas.Add(factura);
                        db2.SaveChanges();
                        foreach (var beneficiario in titular.Beneficiarios)
                        {
                            Facturas_Beneficiarios f_beneficiario = new Facturas_Beneficiarios() { id_beneficiario = beneficiario.id_beneficiario, id_factura = factura.id_factura, id_membresia = beneficiario.id_membresia, valor_a_pagar = beneficiario.Membresias.valor_mensual };
                            db2.Facturas_Beneficiarios.Add(f_beneficiario);
                            db2.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index", new { fecha = inicio.ToString("yyyy-MM-dd") });
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
            catch (Exception ex)
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
