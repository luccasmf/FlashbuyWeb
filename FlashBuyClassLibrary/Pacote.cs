namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pacote")]
    public partial class Pacote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pacote()
        {
            CompraPacote = new HashSet<CompraPacote>();
        }

        [Key]
        public int IdPacote { get; set; }

        public int QtdAnuncio { get; set; }

        public double Valor { get; set; }

        public int DuracaoPacote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraPacote> CompraPacote { get; set; }
    }
}
