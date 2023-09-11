using CleverIgreja.Shared;

namespace CleverIgreja.Models
{
    public class Financeiro
    {
        public int FinanceiroId { get; set; }
        public DateTime DtLanc { get; set; }
        public DateTime DtVenc { get; set; }
        public DateTime DtPagto { get; set; }
        public DateTime DtCompetencia { get; set; }
        public TipoMovimentacaoFinanceira TipoMovimentacaoFinanceira { get; set; }
        public string NumeroDocumento { get; set; }
        public int ParceiroId { get; set; }
        public string Parceiro { get; set; }
        public string Historico { get; set; }
        public float Valor { get; set; }
        public float Desconto { get; set; }
        public float Juros { get; set; }
        public float Total { get; set; }
        public int MoedaId { get; set; }
        public virtual Moeda Moeda { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}