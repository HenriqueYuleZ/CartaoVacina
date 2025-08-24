namespace CartaoVacina.Application.DTOs;

public class VacinacaoDto
{
    public Guid Id { get; set; }
    public Guid PessoaId { get; set; }
    public Guid VacinaId { get; set; }
    public int Dose { get; set; }
    public DateTime DataAplicacao { get; set; }
}

public class CriarVacinacaoDto
{
    public Guid PessoaId { get; set; }
    public Guid VacinaId { get; set; }
    public int Dose { get; set; }
    public DateTime DataAplicacao { get; set; }
}