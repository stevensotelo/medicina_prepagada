using MedicinaPrepagada.Models.DBMedicinaPrepagada.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MedicinaPrepagada.Controllers
{
    public class CuentaController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();
        // GET: Cuentas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cuentas/Details/5
        public ActionResult Details()
        {
            try
            {
                return View(db.CuentasXPagar.ToList());
            }catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            
           
        }

        // GET: Cuentas/Create
        public ActionResult Plano()
        {
            return View();
        }

        // Post subir archivo plano
        [HttpPost]
        public ActionResult Plano(HttpPostedFileBase file)
        {
            try
            {
                string linea;
                char delimeter = ';';

                if (file == null)
                {
                    return View();
                }
                else
                {
                    while ((linea = new StreamReader(file.InputStream).ReadLine()) != null)
                    {
                        string[] subLinea = linea.Split(delimeter);
                        bool isOk = validacion(subLinea.Length, subLinea);
                        if (isOk == false)
                        {
                            //ViewBag.Javascript = ("<Script lenguage= 'javaScript' type = 'text/javaScript'>alert('Revise el archivo de texto, Archivo no valido');</Script>");

                            //ViewData["message"] = "<Script lenguage= 'javaScript' type = 'text/javaScript'>alert('Revise el archivo de texto, Archivo no valido');</Script>";
                            ViewData["message"] = " el archivo de texto, Archivo no valido";
                            return View();
                        }
                        else
                        {
                            DateTime fecha_ini = Convert.ToDateTime(subLinea[0]);
                            DateTime fecha_fin = Convert.ToDateTime(subLinea[1]);
                            int id_ips = int.Parse(subLinea[2]);
                            float valor = float.Parse(subLinea[3]);
                            save(fecha_ini, fecha_fin, id_ips, valor);
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                return View();
            }
            return View();
        }

        public bool validacion(int tamaño,String[] inf)
        {
            DateTime f1;
            int num1;
            float sal;
            if (tamaño == 4 )
             
            {
                if(DateTime.TryParse(inf[0], out f1))
                //if(Regex.IsMatch(inf[0], "[1|2|3]["))
                {
                    if(DateTime.TryParse(inf[1], out f1))
                    {
                        if(int.TryParse(inf[2], out num1))
                        {
                           
                                if (float.TryParse(inf[3], out sal))
                                {
                                    return true;
                                }
                            }
                        }
                       
                    


                }
                
            }
            return false;

        }
        

        //este metodo guarda 
        public bool save(DateTime fecha_ini, DateTime fecha_fin, int ips, float valor)
        {
            try
            {
                CuentasXPagar cuentas = new CuentasXPagar();
                cuentas.fecha_periodo_ini = fecha_ini;
                cuentas.fecha_periodo_fin = fecha_fin;
                cuentas.id_ips = ips;
                

                cuentas.valor_a_pagar = valor;
                cuentas.estado = "0";
                cuentas.pagado = 0;

                //orde los datos
                db.CuentasXPagar.Add(cuentas);
                db.SaveChanges();
                
                return true;
            }
            catch(Exception ex)
            
            {
                return false;
            }
           

            
        }

        // POST: Cuentas/Create
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
                return View();
            }
        }

        // GET: Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CuentasXPagar cuentaTempo = db.CuentasXPagar.Find(id);
                if(cuentaTempo == null)
                {
                    return HttpNotFound();
                }
                return View(cuentaTempo);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // POST: Cuentas/Edit/5
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
                return View();
            }
        }

        // GET: Cuentas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cuentas/Delete/5
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
                return View();
            }
        }
    }
}
