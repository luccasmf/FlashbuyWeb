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
                    TempData["Status"] = "Error";
                    TempData["Message"] = "E-mail já cadastrado na base de dados.";
                    return RedirectToAction("Index","Login");
                }

                anuncianteRep.Salvar(model);

                TempData["Status"] = "Success";
                TempData["Message"] = "Anunciante cadastrado com sucesso!";
                return RedirectToAction("Index","Login");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "Error";
                TempData["Message"] = ex.Message;
                return View("Index");
            }
        }
    }
}