using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashBuy.Controllers
{
    public class CadastroAnuncianteController : Controller
    {
        // GET: CadastroAnunciante
        public ActionResult Index()
        {
            var model = new FlashBuyClassLibrary.Anunciante();
            return View(model);
        }

        [HttpPost]
        public ActionResult Incluir(FlashBuyClassLibrary.Anunciante model) {
            try
            {
                var anuncianteRep = new Repository.AnuncianteRepository();
                if (anuncianteRep.GetAnuncianteByEmail(model.Email))
                {
                    ViewBag.Status = "Error";
                    ViewBag.Message = "E-mail já cadastrado na base de dados.";
                    return View("Index");
                }

                anuncianteRep.Salvar(model);

                ViewBag.Status = "Success";
                ViewBag.Message = "Anunciante cadastrado com sucesso!";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Status = "Error";
                ViewBag.Message = ex.Message;
                return View("Index");
            }
        }
    }
}