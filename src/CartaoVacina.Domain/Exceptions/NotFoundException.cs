namespace CartaoVacina.Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(Guid entityId, string entityName) 
        : base($"{entityName} com ID {entityId} não foi encontrada") { }
}