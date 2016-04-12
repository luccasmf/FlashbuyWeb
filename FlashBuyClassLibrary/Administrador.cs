namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrador")]
    public partial class Administrador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administrador()
        {
            Oferta = new HashSet<Oferta>();
        }

        [Key]
        public int IdAdministrador { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Senha { get; set; }

        public bool Aprovador { get; set; }

        [Required]
        [StringLength(50)]
        public string Permissao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
