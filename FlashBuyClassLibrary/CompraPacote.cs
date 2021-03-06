namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompraPacote")]
    public partial class CompraPacote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompraPacote()
        {
            Oferta = new HashSet<Oferta>();
        }

        [Key]
        [DisplayName("Pacote")]
        public int IdCompraPacote { get; set; }

        public int IdPacote { get; set; }

        public int IdAnunciante { get; set; }

        [DisplayName("Data Validade")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataValidade { get; set; }

        [DisplayName("Data Compra")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataCompra { get; set; }

        public virtual Anunciante Anunciante { get; set; }

        [DisplayName("Anuncios Disponíveis")]
        public int QtdAnuncioDisponivel { get; set; }

        public virtual Pacote Pacote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
