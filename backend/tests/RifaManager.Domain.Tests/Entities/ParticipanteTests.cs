using RifaManager.Domain.Entities;
using Shouldly;

namespace RifaManager.Domain.Tests.Entities;

public sealed class ParticipanteTests
{
    [Fact]
    private void Deve_criar_participante_com_dados_validos()
    {
        Participante? participante = new
        (
            "João",
            "84999999999",
            null
        );

        participante.Id.ShouldNotBe(Guid.Empty);
        participante.Nome.ShouldBe("João");
        participante.Telefone.ShouldBe("84999999999");
        participante.Observacao.ShouldBeNull();
    }
}
