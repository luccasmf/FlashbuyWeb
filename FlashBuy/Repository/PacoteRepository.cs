using FlashBuyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashBuy.Repository
{
    public class PacoteRepository
    {
        FlashBuyModel context = new FlashBuyModel();

        internal IEnumerable<Pacote> GetPacotes()
        {
            return context.Pacote.ToList();
        }

    }
}