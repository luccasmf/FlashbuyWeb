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
        PacoteRepository PacoteRepositorio = new PacoteRepository();
        Uri urlAtual;
        // GET: Anunciante
        public ActionResult Index()
        {
            urlAtual = Request.Url;
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            List<Oferta> ofertas = Anuncianterepositorio.GetOfertasAtivas(AnuncianteSessao.IdAnunciante);
            var listaOfertas = new HashSet<Oferta>(ofertas);
            return View(listaOfertas);
        }

        [HttpGet]
        public ActionResult CriarNova()
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

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
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

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
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            if (Anuncianterepositorio.DeletaOferta(id, AnuncianteSessao.IdAnunciante))
            {
                return RedirectToAction("Index");
            }

            //url.
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
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            Compra c = CompraRepositorio.GetCompraByCod(codigo, AnuncianteSessao.IdAnunciante);  //se o anunciante nao for o dono da oferta, vai retornar null

            if (c == null)
            {
                string[] arr = new string[] { "Error", "Esta venda já foi confirmada ou não existe!" };
                //ViewBag.Status = "Error";
                //ViewBag.Message = "Esta venda já foi confirmada ou não existe!";
                return Json(arr);
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

            if (flag)
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

        public ActionResult Ofertas()
        {

            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            //   *** Alterar linha abaxo: retornar todas as ofertas do anunciante ***
            List<Oferta> ofertas = Anuncianterepositorio.GetOfertas(AnuncianteSessao.IdAnunciante);
            var listaOfertas = new HashSet<Oferta>(ofertas);
            return View(listaOfertas);
        }

        public ActionResult Vendas()
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            List<Compra> compras = Anuncianterepositorio.GetVendasConfirmadas(AnuncianteSessao.IdAnunciante);
            var listaCompras = new HashSet<Compra>(compras);
            return View(listaCompras);
        }

        public ActionResult Pacotes()
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }
            List<CompraPacote> listaPacotesAnunciante = new List<CompraPacote>();
            listaPacotesAnunciante.AddRange(Anuncianterepositorio.GetPacotesAnunciante(AnuncianteSessao.IdAnunciante));
            return View(listaPacotesAnunciante);
        }

        [HttpGet]
        public ActionResult ComprarNovo()
        {
            List<Pacote> listaPacotes = new List<Pacote>();
            listaPacotes.AddRange(PacoteRepositorio.GetPacotes());
            var lista = new List<SelectListItem>();
            foreach (var item in listaPacotes)
            {
                lista.Add(new SelectListItem() { Text = "Valor: R$ " + item.Valor.ToString() + " - Quantidade de anúncios: " + item.QtdAnuncio.ToString() + " - Duração Pacote: " + item.DuracaoPacote.ToString() + " dias", Value = item.IdPacote.ToString() });
            }
            ViewBag.ListaPacotes = lista;
            return View();
        }

        [HttpPost]
        public ActionResult ComprarNovo(int idPacote)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }
            //Luccas, completar o backend por favor!
            //PacoteRepositorio.ConfirmarCompraNovo(AnuncianteSessao.IdAnunciante, idPacote);
            return RedirectToAction("Pacotes");
        }

        public ActionResult Cancelar(int id)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            bool flag = Anuncianterepositorio.MudaStatusOferta(id, AnuncianteSessao.IdAnunciante, EnumOferta.cancelado);

            if (flag)
            {
                ViewBag.Status = "Success";
                ViewBag.Message = "Venda confirmada com susesso!";
            }
            else
            {
                ViewBag.Status = "Error";
                ViewBag.Message = "Ocorreu um erro ao executar esta operação.";
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Desativar(int id)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            bool flag = Anuncianterepositorio.MudaStatusOferta(id, AnuncianteSessao.IdAnunciante, EnumOferta.inativa);

            if (flag)
            {
                ViewBag.Status = "Success";
                ViewBag.Message = "Venda confirmada com susesso!";
            }
            else
            {
                ViewBag.Status = "Error";
                ViewBag.Message = "Ocorreu um erro ao executar esta operação.";
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Reativar(int id)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }


            bool flag = Anuncianterepositorio.MudaStatusOferta(id, AnuncianteSessao.IdAnunciante, EnumOferta.pendente);


            if (flag)
            {
                ViewBag.Status = "Success";
                ViewBag.Message = "Venda confirmada com susesso!";
            }
            else
            {
                ViewBag.Status = "Error";
                ViewBag.Message = "Ocorreu um erro ao executar esta operação.";
            }

            return Redirect(Request.UrlReferrer.ToString());


        }

        [HttpGet]
        public ActionResult EditarOferta(int id)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            Oferta oferta = Anuncianterepositorio.GetOfertaById(id, AnuncianteSessao.IdAnunciante);

            if (oferta != null)
            {
                oferta.Anunciante = AnuncianteSessao;
            }

            return View("EditarOferta", oferta);
        }

        [HttpPost]
        public ActionResult EditarOferta(Oferta oferta, HttpPostedFileBase File, string Latitude, string Longitude)
        {
            var AnuncianteSessao = (Anunciante)Session["AnuncianteSessao"];
            if (AnuncianteSessao == null)
            {
                return Redirect("~/Login");
            }

            Oferta model = Anuncianterepositorio.GetOfertaById(oferta.IdOferta, AnuncianteSessao.IdAnunciante);


            //gambiarra do Luccas
            int id = model.IdOferta;
            var status = model.Status;
            var local = model.LocalOferta;
            var foto = model.Foto;
            var nomeArquivo = model.NomeArquivo;

            if (model.Status != EnumOferta.aprovado)
            {
                model = new Oferta();
                //realizar update da oferta
                model.IdOferta = id;
                model.Produto = oferta.Produto;
                model.Valor = oferta.Valor;
                model.DataInicio = oferta.DataInicio;
                model.DataFim = oferta.DataFim;
                model.Anunciante = AnuncianteSessao;
                model.Status = status;

                if (File != null)
                {
                    model.Foto = converterFileToArray(File);
                    model.NomeArquivo = File.FileName;
                }
                else //gambiarra do Luccas
                {
                    model.Foto = foto;
                    model.NomeArquivo = nomeArquivo;
                }

                if (!string.IsNullOrEmpty(Latitude) && !string.IsNullOrEmpty(Longitude))
                {
                    model.LocalOferta = DbGeography.FromText(string.Format("POINT({0} {1})", Latitude, Longitude));
                }
                else //gambiarra do Luccas
                {
                    model.LocalOferta = local;
                }

                bool flag = Anuncianterepositorio.SalvaOferta(model);

                if (flag)
                {
                    ViewBag.Status = "Success";
                    ViewBag.Message = "Edição realizada com susesso!";
                    return View("EditarOferta", model);
                }
                else
                {
                    ViewBag.Status = "Error";
                    ViewBag.Message = "Ocorreu um erro ao executar esta operação.";
                    return View("EditarOferta", model);
                }
            }
            else
            {
                ViewBag.Status = "Error";
                ViewBag.Message = "Esta oferta já está aprovada e não é possível realizar alterações.";
                return View("EditarOferta", model);
            }
        }
    }
}