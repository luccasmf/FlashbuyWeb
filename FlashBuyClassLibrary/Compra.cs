namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Compra")]
    public partial class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        public int IdCliente { get; set; }

        public int IdOferta { get; set; }

        public DateTime DataHora { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Oferta Oferta { get; set; }
    }
}
