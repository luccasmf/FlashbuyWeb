namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Oferta")]
    public partial class Oferta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oferta()
        {
            Compra = new HashSet<Compra>();
        }

        [Key]
        public int IdOferta { get; set; }

        public int IdAnunciante { get; set; }

        [Required]
        [StringLength(3000)]
        public string Produto { get; set; }

        [Column(TypeName = "image")]
        public byte[] Foto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Valor { get; set; }

        [DisplayName("Início")]
        public DateTime DataInicio { get; set; }

        [DisplayName("Fim")]
        public DateTime DataFim { get; set; }

        [DisplayName("Status da oferta")]
        public EnumOferta Status { get; set; }

        public int IdCompraPacote { get; set; }

        public int? IdAprovador { get; set; }

        [DisplayName("Aprovado em")]
        public DateTime? DataHoraAprovacao { get; set; }

        public virtual Administrador Administrador { get; set; }

        public virtual Anunciante Anunciante { get; set; }
		
        public DbGeography LocalOferta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }

        public virtual CompraPacote CompraPacote { get; set; }

        [StringLength(50)]
        public string NomeArquivo { get; set; }

        [NotMapped]
        public string imgMime { get; set; }
    }
}
