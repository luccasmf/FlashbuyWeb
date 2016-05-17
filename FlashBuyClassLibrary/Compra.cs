namespace FlashBuyClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

        [DisplayName("Data/Hora")]
        public DateTime DataHora { get; set; }

        [DisplayName("Status da compra")]
        public EnumCompra Status { get; set; }

        public bool Votou { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Oferta Oferta { get; set; }
    }
}
