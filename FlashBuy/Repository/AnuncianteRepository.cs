using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlashBuyClassLibrary;

namespace FlashBuy.Repository
{
    public class AnuncianteRepository
    {
        internal List<CompraPacote> GetPacotesAnunciante(int idAnunciante)
        {
            try
            {
                FlashBuyModel context = new FlashBuyModel();
                return context.CompraPacote.Where(c => c.IdAnunciante == idAnunciante).ToList();
            }
            catch (Exception)
            {
                return new List<CompraPacote>();
            }
        }
    }
}