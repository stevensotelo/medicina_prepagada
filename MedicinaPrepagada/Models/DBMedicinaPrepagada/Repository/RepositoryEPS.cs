using MedicinaPrepagada.Models.DBMedicinaPrepagada.Source;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MedicinaPrepagada.Models.DBMedicinaPrepagada.Repository
{
    public class RepositoryEPS
    {
        private EntitiesMedicinaPrepagada db { get; set; }

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        public RepositoryEPS()
        {
            db = new EntitiesMedicinaPrepagada();
        }

        /// <summary>
        /// Metodo que agregar una EPS a la base de datos
        /// </summary>
        /// <param name="entidad">Entidad a agregar</param>
        public void agregar(EPS entidad)
        {
            db.EPS.Add(entidad);
        }

        /// <summary>
        /// Metodo que elimina una EPS de la base de datos
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        public void eliminar(EPS entidad)
        {
            db.EPS.Attach(entidad);
            db.EPS.Remove(entidad);
        }

        /// <summary>
        /// Metodo que edita una EPS en la base de datos
        /// </summary>
        /// <param name="entidad">Entidad a editar</param>
        public void editar(EPS entidad)
        {
            db.Entry(entidad).State = EntityState.Modified;
        }

        /// <summary>
        /// Metodo que guarda los cambios en la base de datos
        /// </summary>
        public void guardar()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Metodo que retorna todas las EPS de la base de datos
        /// </summary>
        /// <returns>Lista de EPS</returns>
        public IQueryable<EPS> listar()
        {
            return db.EPS;
        }

        /// <summary>
        /// Metodo que busca una EPS por su ID
        /// </summary>
        /// <param name="id">Id de la EPS</param>
        /// <returns></returns>
        public EPS buscarPorID(int id)
        {
            return db.EPS.SingleOrDefault(p => p.id_eps == id);
        }
        
    }
}