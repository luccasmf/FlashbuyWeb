using FlashBuyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashBuy.Repository
{
    public class AnuncianteRepository
    {
        internal bool GetAnuncianteByEmail(string email)
        {
            FlashBuyModel context = new FlashBuyModel();
            return context.Anunciante.Any(a => a.Email == email);
        }

        internal void Salvar(Anunciante model)
        {
            try
            {
                FlashBuyModel context = new FlashBuyModel();
                context.Anunciante.Add(model);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}