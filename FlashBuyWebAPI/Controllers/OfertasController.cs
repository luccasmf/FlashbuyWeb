using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlashBuyClassLibrary;

namespace FlashBuyWebAPI.Controllers
{
    public class OfertasController : ApiController
    {
        private FlashBuyModel db = new FlashBuyModel();

        // GET: api/Ofertas
        public IQueryable<Oferta> GetOferta()
        {
            return db.Oferta;
        }

        // GET: api/Ofertas/5
        [ResponseType(typeof(Oferta))]
        public IHttpActionResult GetOferta(int id)
        {
            Oferta oferta = db.Oferta.Find(id);
            if (oferta == null)
            {
                return NotFound();
            }

            return Ok(oferta);
        }

        // PUT: api/Ofertas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOferta(int id, Oferta oferta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oferta.IdOferta)
            {
                return BadRequest();
            }

            db.Entry(oferta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ofertas
        [ResponseType(typeof(Oferta))]
        public IHttpActionResult PostOferta(Oferta oferta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Oferta.Add(oferta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = oferta.IdOferta }, oferta);
        }

        // DELETE: api/Ofertas/5
        [ResponseType(typeof(Oferta))]
        public IHttpActionResult DeleteOferta(int id)
        {
            Oferta oferta = db.Oferta.Find(id);
            if (oferta == null)
            {
                return NotFound();
            }                       

            db.Oferta.Remove(oferta);
            db.SaveChanges();

            return Ok(oferta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfertaExists(int id)
        {
            return db.Oferta.Count(e => e.IdOferta == id) > 0;
        }
    }
}