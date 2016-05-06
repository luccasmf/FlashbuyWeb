using System.ComponentModel;

namespace FlashBuyClassLibrary
{
    public enum EnumOferta : byte
    {
        [Description("Pendente")]
        pendente = 1,
        [Description("Aprovado")]
        aprovado,
        [Description("Reprovado")]
        reprovado,
        [Description("Expirado")]
        expirado,
        [Description("Cancelado")]
        cancelado,
        [Description("Inativa")]
        inativa,

    }
}