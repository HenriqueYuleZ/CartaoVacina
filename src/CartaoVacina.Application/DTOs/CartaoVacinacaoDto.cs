namespace CartaoVacina.Application.DTOs;

public class CartaoVacinacaoDto
{
    public PessoaDto Pessoa { get; set; } = null!;
    public List<VacinacaoDetalhadaDto> Vacinacoes { get; set; } = new();
}

public class VacinacaoDetalhadaDto
{
    public Guid Id { get; set; }
    public int Dose { get; set; }
    public DateTime DataAplicacao { get; set; }
    public VacinaDto Vacina { get; set; } = null!;
}
