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
    public class ComprasController : ApiController
    {
        private FlashBuyModel db = new FlashBuyModel();

        // GET: api/Compras
        public IQueryable<Compra> GetCompra()
        {
            return db.Compra;
        }

        // GET: api/Compras/5
        [ResponseType(typeof(Compra))]
        public IHttpActionResult GetCompra(int id)
        {
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return NotFound();
            }

            return Ok(compra);
        }

        // GET: api/Compras/GetComprasCliente?idCliente=1234
       // [ResponseType(typeof(Compra[]))]
        public IHttpActionResult GetComprasCliente(int idCliente)
        {
            OfertasController oc = new OfertasController();
             List<Compra> comprasDoCliente = db.Compra.Where(p => p.IdCliente == idCliente).ToList();

            foreach (Compra c in comprasDoCliente)
            {
                if (c.Oferta == null)
                {
                    c.Oferta = db.Oferta.FirstOrDefault(p => p.IdOferta == c.IdOferta);
                    c.Oferta.Compra = null;
                }
                try
                {
                    c.Oferta.imgMime = imgToString64(c.Oferta);
                    c.Oferta.Foto = null;
                }
                catch { }            
            }

           

            if (comprasDoCliente.Count() == 0)
            {
                return NotFound();
            }
            return Ok(comprasDoCliente);
        }

        // PUT: api/Compras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompra(int id, Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compra.IdCompra)
            {
                return BadRequest();
            }

            db.Entry(compra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/Compras
        [ResponseType(typeof(Compra))]
        public IHttpActionResult PostCompra(Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Compra.Add(compra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = compra.IdCompra }, compra);
        }

        // DELETE: api/Compras/5
        [ResponseType(typeof(Compra))]
        public IHttpActionResult DeleteCompra(int id)
        {
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return NotFound();
            }

            db.Compra.Remove(compra);
            db.SaveChanges();

            return Ok(compra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompraExists(int id)
        {
            return db.Compra.Count(e => e.IdCompra == id) > 0;
        }

        // POST: api/PostGeraCompra/?idOferta=1234&idCliente=1234
        public int PostGeraCompra(int idOferta, int idCliente)
        {
            Compra c = db.Compra.FirstOrDefault(p => p.IdCliente == idCliente && p.IdOferta == idOferta);

            if (c == null)
            {
                c = new Compra();
                c.IdCliente = idCliente;
                c.IdOferta = idOferta;
                c.Status = EnumCompra.Visualizado;
                c.DataHora = DateTime.Now;
                c.Votou = false;

                db.Compra.Add(c);
                db.SaveChanges();
            }
            return c.IdCompra;
        }
        
        // POST: api/PostCheckarCompra/?idCompra=1234
        public bool PostCheckarCompra(int idCompra)
        {
            Compra c = db.Compra.Find(idCompra);

            c.Status = EnumCompra.Checkado;
            c.DataHora = DateTime.Now;

            return (db.SaveChanges()>0);
        }

        private string imgToString64(Oferta entidade)
        {

            return String.Concat("data:", System.Web.MimeMapping.GetMimeMapping(entidade.NomeArquivo), ";base64,", System.Convert.ToBase64String(entidade.Foto));

        }


    }
}