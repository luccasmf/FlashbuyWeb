namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Anunciante")]
    public partial class Anunciante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Anunciante()
        {
            CompraPacote = new HashSet<CompraPacote>();
            Oferta = new HashSet<Oferta>();
        }

        [Key]
        public int IdAnunciante { get; set; }

        [StringLength(50)]
        public string CPF_CNPJ { get; set; }

        [Required]
        [StringLength(50)]
        public string RazaoSocial { get; set; }

        [StringLength(50)]
        public string NomeFantasia { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraPacote> CompraPacote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
