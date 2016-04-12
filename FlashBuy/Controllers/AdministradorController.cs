using FlashBuy.Repository;
using FlashBuyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashBuy.Controllers
{
    public class AdministradorController : Controller
    {
        AdministradorRepository repositorio = new AdministradorRepository();

        // GET: Administrador
        public ActionResult Index()
        {
            List<Oferta> ofertas = repositorio.GetOfertas();
            var listaOfertas = new HashSet<Oferta>(ofertas);
            return View(listaOfertas);
        }

        [HttpGet]
        public ActionResult AprovarOferta(int id)
        {

            var AdministradorSessao = (Administrador)Session["AdministradorSessao"];
            repositorio.AprovaReprovaOferta(true, id, AdministradorSessao.IdAdministrador);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ReprovarOferta(int id)
        {
            var AdministradorSessao = (Administrador)Session["AdministradorSessao"];
            repositorio.AprovaReprovaOferta(false, id, AdministradorSessao.IdAdministrador);
            return RedirectToAction("Index");
        }
    }
}