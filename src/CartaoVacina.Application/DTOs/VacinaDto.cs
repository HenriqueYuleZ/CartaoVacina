namespace CartaoVacina.Application.DTOs;
public class VacinaDto
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
}

public class CriarVacinaDto
{
    public required string Nome { get; set; }
}