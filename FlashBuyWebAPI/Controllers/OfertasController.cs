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

        // GET: api/Ofertas/GetOferta
        public IHttpActionResult GetOferta()
        {
            List<Oferta> oferta = db.Oferta.Where(p => p.Status == EnumOferta.aprovado).ToList();

            foreach (Oferta of in oferta)
            {
                of.Compra = null;
                of.Administrador = null;
                of.CompraPacote = null;
                of.IdAprovador = null;
                of.DataHoraAprovacao = null;

                Anunciante a = db.Anunciante.FirstOrDefault(p => p.IdAnunciante == of.IdAnunciante);
                of.Anunciante.IdAnunciante = a.IdAnunciante;
                of.Anunciante = new Anunciante();
                of.Anunciante.NomeFantasia = a.NomeFantasia;
                of.Anunciante.Email = a.Email;
                of.Anunciante.Telefone = a.Telefone;
                of.imgMime = imgToString64(of);

                of.Foto = null;
            }

            if (oferta.Count == 0)
            {
                return NotFound();
            }
            return Ok(oferta);
        }

        // GET: api/Ofertas/GetOferta?id=1
        public IHttpActionResult GetOferta(int id)
        {
            Oferta oferta = db.Oferta.Find(id);
            oferta.Administrador = null;
            oferta.Compra = null;
            oferta.CompraPacote = null;
            oferta.IdAprovador = null;
            oferta.DataHoraAprovacao = null;
            Anunciante a = db.Anunciante.FirstOrDefault(p => p.IdAnunciante == oferta.IdAnunciante);
            oferta.Anunciante.IdAnunciante = a.IdAnunciante;
            oferta.Anunciante = new Anunciante();
            oferta.Anunciante.NomeFantasia = a.NomeFantasia;
            oferta.Anunciante.Email = a.Email;
            oferta.Anunciante.Telefone = a.Telefone;
            oferta.imgMime = imgToString64(oferta);
            oferta.Foto = null;


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

        private string imgToString64(Oferta entidade)
        {
                    
            return String.Concat("data:", System.Web.MimeMapping.GetMimeMapping(entidade.NomeArquivo), ";base64,", System.Convert.ToBase64String(entidade.Foto));
            
        }
    }
}