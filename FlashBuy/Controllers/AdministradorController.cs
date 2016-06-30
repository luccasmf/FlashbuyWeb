using FlashBuy.Repository;
using FlashBuyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashBuy.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        AdministradorRepository repositorio = new AdministradorRepository();

        // GET: Administrador
        public ActionResult Index()
        {
            var AdministradorSessao = (Administrador)Session["AdministradorSessao"];
            if (AdministradorSessao == null)
            {
                return Redirect("~/Login");
            }

            List<Oferta> ofertas = repositorio.GetOfertasPendentes();
            var listaOfertas = new HashSet<Oferta>(ofertas);
            return View(listaOfertas);
        }

        [HttpGet]
        public ActionResult AprovarOferta(int id)
        {

            var AdministradorSessao = (Administrador)Session["AdministradorSessao"];
            if (AdministradorSessao == null)
            {
                return Redirect("~/Login");
            }


            repositorio.AprovaReprovaOferta(true, id, AdministradorSessao.IdAdministrador);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ReprovarOferta(int id)
        {
            var AdministradorSessao = (Administrador)Session["AdministradorSessao"];
            if (AdministradorSessao == null)
            {
                return Redirect("~/Login");
            }
            repositorio.AprovaReprovaOferta(false, id, AdministradorSessao.IdAdministrador);
            return RedirectToAction("Index");
        }

        public ActionResult Ofertas()
        {
            List<Oferta> ofertas = repositorio.GetOfertas();
            var listaOfertas = new HashSet<Oferta>(ofertas);
            return View(listaOfertas);
        }

        public ActionResult Anunciantes()
        {
            List<Anunciante> anunciantes = repositorio.GetAnunciantes();
            var listaAnunciantes = new HashSet<Anunciante>(anunciantes);
            return View(listaAnunciantes);
        }

        public ActionResult Usuarios()
        {
            List<Cliente> clientes = repositorio.GetClientes();
            var listaClientes = new HashSet<Cliente>(clientes);
            return View(listaClientes);
        }
    }
}