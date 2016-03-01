namespace FlashBuyClassLibrary
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FlashBuyModel : DbContext
    {
        public FlashBuyModel()
            : base("name=FlashBuyModel")
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Anunciante> Anunciante { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<CompraPacote> CompraPacote { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Pacote> Pacote { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .Property(e => e.Permissao)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .HasMany(e => e.Oferta)
                .WithOptional(e => e.Administrador)
                .HasForeignKey(e => e.IdAprovador);

            modelBuilder.Entity<Anunciante>()
                .Property(e => e.CPF_CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<Anunciante>()
                .Property(e => e.RazaoSocial)
                .IsUnicode(false);

            modelBuilder.Entity<Anunciante>()
                .Property(e => e.NomeFantasia)
                .IsUnicode(false);

            modelBuilder.Entity<Anunciante>()
                .Property(e => e.Telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Anunciante>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Anunciante>()
                .HasMany(e => e.CompraPacote)
                .WithRequired(e => e.Anunciante)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anunciante>()
                .HasMany(e => e.Oferta)
                .WithRequired(e => e.Anunciante)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.IMEI)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Compra)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompraPacote>()
                .HasMany(e => e.Oferta)
                .WithRequired(e => e.CompraPacote)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oferta>()
                .Property(e => e.Produto)
                .IsUnicode(false);

            modelBuilder.Entity<Oferta>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Oferta>()
                .HasMany(e => e.Compra)
                .WithRequired(e => e.Oferta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pacote>()
                .HasMany(e => e.CompraPacote)
                .WithRequired(e => e.Pacote)
                .WillCascadeOnDelete(false);
        }
    }
}
