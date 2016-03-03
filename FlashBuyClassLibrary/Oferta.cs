namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
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
        [Required]
        public byte[] Foto { get; set; }

        public double Valor { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public EnumOferta Status { get; set; }

        public int IdCompraPacote { get; set; }

        public int? IdAprovador { get; set; }

        public DateTime? DataHoraAprovacao { get; set; }

        public virtual Administrador Administrador { get; set; }

        public virtual Anunciante Anunciante { get; set; }
		
		[StringLength(100)]
        public string LocalOferta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }

        public virtual CompraPacote CompraPacote { get; set; }
    }
}
