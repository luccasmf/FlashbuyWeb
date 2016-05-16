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

        internal bool ConfirmarCompraNovo(int idAnunciante, int idPacote)
        {
            Pacote p = context.Pacote.Find(idPacote);

            CompraPacote cp = new CompraPacote();
            cp.IdAnunciante = idAnunciante;
            cp.IdPacote = idPacote;
            cp.DataCompra = DateTime.Now;
            cp.DataValidade = DateTime.Now.AddDays(p.DuracaoPacote);
            cp.QtdAnuncioDisponivel = p.QtdAnuncio;

            context.CompraPacote.Add(cp);            

            return (context.SaveChanges()>0);
        }
    }
}