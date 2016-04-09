using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlashBuyClassLibrary;
using FlashBuy.Repository;
using System.IO;
using System.Drawing;

namespace FlashBuy.Controllers
{
    public class AnuncianteController : Controller
    {
        AnuncianteRepository repositorio = new AnuncianteRepository();
        // GET: Anunciante
        public ActionResult Index()
        {
            var listaOfertas = new HashSet<Oferta>();
            return View(listaOfertas);
        }

        [HttpGet]
        public ActionResult CriarNova()
        {
            
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            List<CompraPacote> listaPacotesAnunciante = new List<CompraPacote>();
            listaPacotesAnunciante.AddRange(repositorio.GetPacotesAnunciante(AnuncianteSessao.IdAnunciante));
            var SelectListItem = new List<SelectListItem>();
            foreach (var item in listaPacotesAnunciante)
            {
                SelectListItem slc = new SelectListItem();
                slc.Text = "Validade: " + item.DataValidade + " - Anuncios restantes: " + item.QtdAnuncioDisponivel;
                slc.Value = item.IdCompraPacote.ToString();

                SelectListItem.Add(slc);
            }
            ViewBag.listaPacotesAnunciante = SelectListItem;
            return View();
        }

        [HttpPost]
        public ActionResult CriarNova(Oferta NovaOferta, HttpPostedFileBase File)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];

            NovaOferta.Foto = converterFileToArray(File);
            NovaOferta.IdAnunciante = AnuncianteSessao.IdAnunciante;
            NovaOferta.Status = EnumOferta.pendente;

            if(repositorio.CriaNovaOferta(NovaOferta))
                return RedirectToAction("Index");
            else
                return View();
        }

        public static byte[] converterFileToArray(HttpPostedFileBase x)
        {
            MemoryStream tg = new MemoryStream();
            x.InputStream.CopyTo(tg);
            byte[] data = tg.ToArray();

            return data;
        }

        
    }
}