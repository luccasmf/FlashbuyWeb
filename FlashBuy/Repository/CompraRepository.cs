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
        internal Compra GetCompraByCod(int codigo, int idAnunciante)
        {
            Compra c = new Compra();

            c = context.Compra.Find(codigo);            
            c.Cliente = context.Cliente.Find(c.IdCliente);
            c.Oferta = context.Oferta.Find(c.IdOferta);

            if(c.Oferta.IdAnunciante != idAnunciante)
            {
                c = null;
            }

             var query = (from a in context.Compra
                         join b in context.Cliente on a.IdOferta equals b.IdCliente
                         join d in context.Oferta on a.IdOferta equals d.IdOferta
                         select new { a, b.Nome, d });

            return (c);


        }

        internal bool CompletaVenda(int codigo)
        {
            Compra c = new Compra();

            c = context.Compra.Find(codigo);

            c.Status = EnumCompra.Confirmado;

            if(context.SaveChanges()>1)
            {
                return true;
            }

            return false;
        }
    }
}