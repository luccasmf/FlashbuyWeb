using FlashBuyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashBuy.Repository
{
    public class AnuncianteRepository
    {
        FlashBuyModel context = new FlashBuyModel();
        internal bool GetAnuncianteByEmail(string email)
        {
            
            return context.Anunciante.Any(a => a.Email == email);
        }

        internal void Salvar(Anunciante model)
        {
            try
            {
                model.Senha = Util.Helper.GerarHashMd5(model.Senha);
                FlashBuyModel context = new FlashBuyModel();
                context.Anunciante.Add(model);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public List<CompraPacote> GetPacotesAnunciante(int idAnunciante)
        {
            return context.CompraPacote.Where(p => p.IdAnunciante == idAnunciante && p.DataValidade >= DateTime.Now).ToList();
        }
    }
}