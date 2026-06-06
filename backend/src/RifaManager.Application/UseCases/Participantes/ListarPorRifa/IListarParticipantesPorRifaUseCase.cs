namespace RifaManager.Application.UseCases.Participantes.ListarPorRifa;

public interface IListarParticipantesPorRifaUseCase
{
    Task<IReadOnlyList<ListarParticipantesPorRifaResponse>> Execute(Guid rifaId);
}
