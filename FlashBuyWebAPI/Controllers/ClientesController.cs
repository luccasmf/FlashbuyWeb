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
    public class ClientesController : ApiController
    {
        private FlashBuyModel db = new FlashBuyModel();

        // GET: api/Clientes
        public IQueryable<Cliente> GetCliente()
        {
            return db.Cliente;
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }



        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cliente.Add(cliente);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.IdCliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.IdCliente }, cliente);
        }

        /// <summary>
        /// valida se usuário já está cadastrado, caso não esteja, cadastra no banco
        /// </summary>
        /// <param name="IMEI">IMEI ou MAC ADDRESS codificado em MD5</param>
        /// <returns></returns>
        // POST: api/Clientes/PostLogin?IMEI=432f45b44c432414d2f97df0e5743818&nome=xxxxx
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostLogin(string IMEI,string nome, string deviceKey) //IMEI codificado em MD5
        {

            IMEI = IMEI.Trim();
            try
            {
                var c = db.Cliente.Where(cc => cc.IMEI == IMEI);
                if (c.Count() >=1 )
                    return Ok(c);
                else
                    throw new Exception("Usuário Inexistente");
            }
            catch(Exception e)
            {
                if (e.Message == "Usuário Inexistente")
                {
                    Cliente c = new Cliente();
                    c.IMEI = IMEI;
                    c.Nome = nome;
                    c.DeviceKey = deviceKey;

                    db.Cliente.Add(c);
                    if (db.SaveChanges() > 0)
                        return Ok(c);
                    else
                        return NotFound();
                }
                else
                    return NotFound();
            }
        }

        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostLogin(string IMEI) //IMEI codificado em MD5
        {
            IMEI = IMEI.Trim();
            var c = db.Cliente.Where(cc => cc.IMEI == IMEI);
            if (c.Count() >= 1)
                return Ok(c);
            else
                return Ok(c);
            
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.IdCliente == id) > 0;
        }

        public IHttpActionResult PostVotaAnunciante(int idCompra, bool voto)
        {
            Compra c = db.Compra.Find(idCompra);
            Oferta o = db.Oferta.Find(c.IdOferta);
            Anunciante a = db.Anunciante.Find(o.IdAnunciante);

            if(voto)
            {
                a.VotoPositivo++;
            }
            else
            {
                a.VotoPositivo--;
            }

            c.Votou = true;

            try
            {
                db.SaveChanges();                
                return Ok(true);
            }
            catch
            {
                return NotFound();
            }
        }

        
    }
}