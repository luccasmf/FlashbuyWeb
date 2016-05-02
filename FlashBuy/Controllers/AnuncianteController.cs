using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlashBuyClassLibrary;
using FlashBuy.Repository;
using System.IO;
using System.Drawing;
using System.Data.Entity.Spatial;

namespace FlashBuy.Controllers
{
    public class AnuncianteController : Controller
    {
        AnuncianteRepository Anuncianterepositorio = new AnuncianteRepository();
        CompraRepository CompraRepositorio = new CompraRepository();

        // GET: Anunciante
        public ActionResult Index()
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            List<Oferta> ofertas = Anuncianterepositorio.GetOfertasAtivas(AnuncianteSessao.IdAnunciante);
            var listaOfertas = new HashSet<Oferta>(ofertas);
            return View(listaOfertas);
        }

        [HttpGet]
        public ActionResult CriarNova()
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            List<CompraPacote> listaPacotesAnunciante = new List<CompraPacote>();
            listaPacotesAnunciante.AddRange(Anuncianterepositorio.GetPacotesAnunciante(AnuncianteSessao.IdAnunciante));
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
        public ActionResult CriarNova(Oferta NovaOferta, HttpPostedFileBase File, string Latitude, string Longitude)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];

            NovaOferta.Foto = converterFileToArray(File);
            NovaOferta.IdAnunciante = AnuncianteSessao.IdAnunciante;
            NovaOferta.Status = EnumOferta.pendente;
            NovaOferta.NomeArquivo = File.FileName;
            NovaOferta.LocalOferta = DbGeography.FromText(string.Format("POINT({0} {1})", Latitude, Longitude));

            if (Anuncianterepositorio.CriaNovaOferta(NovaOferta))
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

        public ActionResult Delete(int id)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];

            if (Anuncianterepositorio.DeletaOferta(id, AnuncianteSessao.IdAnunciante))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Venda()
        {
            return View();
        }

        public ActionResult BuscarCompra(int codigo)
        {
            //var model = new Compra();
            //model.IdCompra = 1;
            //model.Cliente = new Cliente { Nome = "Nome Cliente" };
            //model.Oferta = new Oferta
            //{
            //    Status = EnumOferta.cancelado,
            //    Anunciante = new Anunciante { NomeFantasia = "Nome" },
            //    Produto = "Produto",
            //    Valor = 100.00,
            //    DataInicio = DateTime.Now,
            //    DataFim = DateTime.Now
            //};

            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];            
            Compra c = CompraRepositorio.GetCompraByCod(codigo,AnuncianteSessao.IdAnunciante);  //se o anunciante nao for o dono da oferta, vai retornar null

            if (c == null)
            {
                ViewBag.Status = "Error";
                ViewBag.Message = "Esta venda já foi confirmada ou não existe!";
                return View("Venda");
            }
            else
            {

                return PartialView("_DadosCompra", c);
            }
        }

        [HttpPost]
        public ActionResult ConfirmarCompra(Compra compra)
        {
            bool flag = CompraRepositorio.CompletaVenda(compra.IdCompra);

            if(flag)
            {
                ViewBag.Status = "Success";
                ViewBag.Message = "Venda confirmada com susesso!";
                return View("Venda");
            }
            else
            {
                ViewBag.Status = "Error";
                ViewBag.Message = "Ocorreu um erro ao executar esta operação.";
                return View("Venda");
            }

            
        }
    }
}