using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlashBuyClassLibrary;

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
    }
}