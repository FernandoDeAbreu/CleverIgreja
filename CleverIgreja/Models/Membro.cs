using CleverIgreja.Shared;

namespace CleverIgreja.Models
{
    public class Membro
    {
        public int MembroId { get; set; }
        public string Nome { get; set; }
        public TipoPessoa? TipoPessoa { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? CnpjCpf { get; set; }
        public string? RgIe { get; set; }
        public string? Cep { get; set; }
        public string? Cidade { get; set; }
        public UF? UF { get; set; }
        public string? Bairro { get; set; }
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Celular { get; set; }
        public string? Whatsapp { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Observacao { get; set; }
    }
}