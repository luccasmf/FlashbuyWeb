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
                model.VotoNegativo = 0;
                model.VotoPositivo = 0;
                FlashBuyModel context = new FlashBuyModel();
                context.Anunciante.Add(model);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        internal List<CompraPacote> GetPacotesAnunciante(int idAnunciante)
        {
            return context.CompraPacote.Where(p => p.IdAnunciante == idAnunciante && p.DataValidade >= DateTime.Now).ToList();
        }

        internal bool CriaNovaOferta(Oferta of)
        {
            CompraPacote cc = context.CompraPacote.Where(cp => cp.IdCompraPacote == of.IdCompraPacote).FirstOrDefault();

            if (SalvaOferta(of))
            {
                cc.QtdAnuncioDisponivel--;
                return true;
            }
            return false;

          
        }

        internal bool SalvaOferta(Oferta oferta)
        {
            if (!(oferta.IdOferta >= 0))
            {
                context.Oferta.Add(oferta);
            }
            else
            {
                Oferta of = context.Oferta.Where(o => o.IdOferta == oferta.IdOferta).ToList()[0];
                //Oferta of = context.Oferta.Find(oferta.IdOferta);
                of.Produto = oferta.Produto;
                of.Valor = oferta.Valor;
                of.DataInicio = oferta.DataInicio;
                of.DataFim = oferta.DataFim;
                of.Foto = oferta.Foto;
                of.NomeArquivo = oferta.NomeArquivo;
                of.LocalOferta = oferta.LocalOferta;
            }

            if (context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        internal bool DeletaOferta(int idOferta, int idAnunciante)
        {
            Oferta of = context.Oferta.FirstOrDefault(o => o.IdAnunciante == idAnunciante && o.IdOferta == idOferta);
            context.Oferta.Remove(of);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        internal List<Oferta> GetOfertasAtivas(int idAnunciante)
        {
            return context.Oferta.Where(of => of.IdAnunciante == idAnunciante && (of.Status == EnumOferta.aprovado || of.Status == EnumOferta.pendente)).OrderByDescending(of => of.Status).ToList();
        }

        internal List<Compra> GetVendasConfirmadas(int idAnunciante)
        {
            List<Compra> compras = context.Compra.Where(cp => cp.Oferta.IdAnunciante == idAnunciante && (cp.Status == EnumCompra.Confirmado)).OrderByDescending(cp => cp.IdCompra).ToList();

            
            foreach (Compra c in compras)
            {
                c.Cliente = context.Cliente.Find(c.IdCliente);
                c.Oferta = context.Oferta.Find(c.IdOferta);
            }

            return compras;
        }

        internal List<Oferta> GetOfertas(int idAnunciante)
        {
            return context.Oferta.Where(of => of.IdAnunciante == idAnunciante).OrderByDescending(of => of.DataInicio).ToList();
        }

        internal bool MudaStatusOferta(int idOferta, int idAnunciante, EnumOferta status)
        {
            Oferta of = context.Oferta.Find(idOferta);

            if(of.IdAnunciante == idAnunciante)
            {
                of.Status = status;
                of.DataHoraAprovacao = null;

                if(DateTime.Now > of.DataFim)
                {
                    return false;
                }

                if(context.SaveChanges()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        internal Oferta GetOfertaById(int idOferta, int idAnunciante)
        {
            Oferta oferta = context.Oferta.Find(idOferta);
            if(!(oferta.IdAnunciante == idAnunciante))
            {
                oferta = null;
            }

            return oferta;
        }
    }
}