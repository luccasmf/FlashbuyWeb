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

            if(c==null)
            {
                return c;
            }

            c.Cliente = context.Cliente.Find(c.IdCliente);
            c.Oferta = context.Oferta.Find(c.IdOferta);

            if (c.Oferta.IdAnunciante != idAnunciante || c.Status == EnumCompra.Confirmado)
            {
                c = null;
            }

            return (c);
            
        }

        internal bool CompletaVenda(int codigo)
        {
            Compra c = new Compra();

            c = context.Compra.Find(codigo);
            c.Status = EnumCompra.Confirmado;
            c.DataHora = DateTime.Now;

            if(context.SaveChanges()>0)
            {
                return true;
            }

            return false;
        }
    }
}