using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlashBuyClassLibrary;

namespace FlashBuy.Repository
{
    public class LoginRepository
    {
        internal Anunciante GetAnunciante(string email)
        {
            FlashBuyModel context = new FlashBuyModel();
            return context.Anunciante.Single(a => a.Email == email);
        }

        internal Administrador GetAdministrador(string email)
        {
            FlashBuyModel context = new FlashBuyModel();
            return context.Administrador.Single(a => a.Email == email);
        }
    }
}