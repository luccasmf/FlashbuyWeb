using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashBuy.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            var listaTiposUsuarios = new List<SelectListItem>();
            listaTiposUsuarios.Add(new SelectListItem() { Text = "Anunciante", Value = "Anunciante" });
            listaTiposUsuarios.Add(new SelectListItem() { Text = "Administrador", Value = "Administrador" });
            ViewBag.listaTiposUsuarios = listaTiposUsuarios;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email, string Senha, string TipoUsuario)
        {
            try
            {
                if(TipoUsuario == "Anunciante")
                {
                    //Processar login para ANUNCIANTE
                    return RedirectToAction("Index","Anunciante");
                }
                else
                {
                    //Processar login para ADMINISTRADOR
                    return RedirectToAction("Index", "Administrador");
                }
            }
            catch (Exception)
            {
                ViewBag.MsgErro = "Login inválido";
                throw;
            }
        }
    }
}