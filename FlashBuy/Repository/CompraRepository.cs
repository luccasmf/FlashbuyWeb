using FlashBuyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashBuy.Repository
{
    public class CompraRepository
    {
        FlashBuyModel context = new FlashBuyModel();
        internal Compra GetCompraByCod(string codigo)
        {
            return (new Compra());
        }
    }
}