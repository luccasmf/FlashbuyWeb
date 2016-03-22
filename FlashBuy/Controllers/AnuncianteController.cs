using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlashBuyClassLibrary;
using FlashBuy.Repository;

namespace FlashBuy.Controllers
{
    public class AnuncianteController : Controller
    {
        // GET: Anunciante
        public ActionResult Index()
        {
            var listaOfertas = new HashSet<Oferta>();
            return View(listaOfertas);
        }

        [HttpGet]
        public ActionResult CriarNova()
        {
            AnuncianteRepository repositorio = new AnuncianteRepository();
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            List<CompraPacote> listaPacotesAnunciante = new List<CompraPacote>();//repositorio.GetPacotesAnunciante(AnuncianteSessao.IdAnunciante);
            var SelectListItem = new List<SelectListItem>();
            foreach (var item in listaPacotesAnunciante)
            {
                SelectListItem.Add(new SelectListItem() { Text = "Validade: " + item.DataValidade + " - Anuncios restantes: " + item.Pacote.QtdAnuncio, Value = item.IdCompraPacote.ToString() });
            }
            ViewBag.listaPacotesAnunciante = SelectListItem;
            return View();
        }

        [HttpPost]
        public ActionResult CriarNova(Oferta NovaOferta)
        {
            return View();
        }
    }
}