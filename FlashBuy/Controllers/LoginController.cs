﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlashBuy.Repository;
using FlashBuyClassLibrary;
using System.Web.Security;

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

            if (TempData["Status"] != null)
            {
                ViewBag.Status = TempData["Status"].ToString();
                TempData.Remove("Status");
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData.Remove("Message");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email, string Senha, string TipoUsuario)
        {
            try
            {
                LoginRepository repositorio = new LoginRepository();
                if (TipoUsuario == "Anunciante")
                {
                    Anunciante anunciante = repositorio.GetAnunciante(Email);
                    if (anunciante.Senha == Util.Helper.GerarHashMd5(Senha))
                    {
                        FormsAuthentication.SetAuthCookie(anunciante.Email, true);
                        Session.Add("AnuncianteSessao", anunciante);
                        return RedirectToAction("Index", "Anunciante");
                    }
                }
                else
                {
                    Administrador administrador = repositorio.GetAdministrador(Email);
                    if (administrador.Senha == Util.Helper.GerarHashMd5(Senha))
                    {
                        FormsAuthentication.SetAuthCookie(administrador.Email, true);
                        Session.Add("AdministradorSessao", administrador);
                        return RedirectToAction("Index", "Administrador");
                    }
                }
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                var listaTiposUsuarios = new List<SelectListItem>();
                listaTiposUsuarios.Add(new SelectListItem() { Text = "Anunciante", Value = "Anunciante" });
                listaTiposUsuarios.Add(new SelectListItem() { Text = "Administrador", Value = "Administrador" });
                ViewBag.listaTiposUsuarios = listaTiposUsuarios;
                ViewBag.MsgErro = "* Login inválido";
                return View();
            }
        }

        public ActionResult Sair()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}