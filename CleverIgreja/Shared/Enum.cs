using System.ComponentModel.DataAnnotations;

namespace CleverIgreja.Shared
{
    public enum Enum
    {
    }

    public enum TipoPessoa
    {
        [Display(Name = "Física")]
        Fisica,

        [Display(Name = "Jurídica")]
        Juridica
    }

    public enum TipoMovimentacaoFinanceira
    {
        Receita,
        Despesa
    }

    public enum UF 
    {
        AC,
        AL,
        AP,
        AM,
        BA,
        CE,
        DF,
        ES,
        GO,
        MA,
        MT,
        MS,
        MG,
        PA,
        PB,
        PR,
        PE,
        PI,
        RJ,
        RN,
        RS,
        RO,
        RR,
        SC,
        SP,
        SE,
        TO
    }
}