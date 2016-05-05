using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlashBuyClassLibrary;

namespace FlashBuy.Repository
{
    public class AdministradorRepository
    {
        FlashBuyModel context = new FlashBuyModel();
        internal List<Oferta> GetOfertasPendentes()
        {
            return context.Oferta.Where(o => o.Status == EnumOferta.pendente || o.Status == EnumOferta.reprovado).OrderBy(o => o.Status).ToList();
        }

        internal bool AprovaReprovaOferta(bool status, int idOferta, int idAdministrador)
        {
            Oferta oferta = context.Oferta.Find(idOferta);
            if(status)
            {
                oferta.Status = EnumOferta.aprovado;
            }

            else
            {
                oferta.Status = EnumOferta.reprovado;
            }

            oferta.IdAprovador = idAdministrador;
            oferta.DataHoraAprovacao = DateTime.Now;

            return(context.SaveChanges()>1);
        }

        internal List<Anunciante> GetAnunciantes()
        {
            return context.Anunciante.ToList();
        }

        internal List<Oferta> GetOfertas()
        {
            return context.Oferta.OrderBy(o => o.DataFim).ToList();
        }

        internal List<Cliente> GetClientes()
        {
            return context.Cliente.ToList();
        }
    }
}