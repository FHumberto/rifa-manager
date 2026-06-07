using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;
using RifaManager.Application.UseCases.Bilhetes.CancelarBilhete;
using RifaManager.Application.UseCases.Bilhetes.GetById;
using RifaManager.Application.UseCases.Bilhetes.ListarPorRifa;
using RifaManager.Application.UseCases.Bilhetes.ListarPorStatus;
using RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;
using RifaManager.Application.UseCases.Login;
using RifaManager.Application.UseCases.Participantes.CadastrarParticipante;
using RifaManager.Application.UseCases.Participantes.EditarParticipante;
using RifaManager.Application.UseCases.Participantes.GetById;
using RifaManager.Application.UseCases.Participantes.ListarPorRifa;
using RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;
using RifaManager.Application.UseCases.Rifas.CadastrarRifa;
using RifaManager.Application.UseCases.Rifas.EditarRifa;
using RifaManager.Application.UseCases.Rifas.EncerrarRifa;
using RifaManager.Application.UseCases.Rifas.GetById;
using RifaManager.Application.UseCases.Rifas.ListarRifas;
using RifaManager.Application.UseCases.Rifas.SortearRifa;
using RifaManager.Application.UseCases.Usuarios.AtivarUsuario;
using RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;
using RifaManager.Application.UseCases.Usuarios.DesativarUsuario;
using RifaManager.Application.UseCases.Usuarios.EditarUsuario;
using RifaManager.Application.UseCases.Usuarios.GetById;

namespace RifaManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddUseCases();

        return services;
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginHandler>();
        services.AddScoped<IAlterarStatusBilheteUseCase, AlterarStatusBilheteHandler>();
        services.AddScoped<ICancelarBilheteUseCase, CancelarBilheteHandler>();
        services.AddScoped<IGetBilheteByIdUseCase, GetBilheteByIdHandler>();
        services.AddScoped<IListarBilhetesPorRifaUseCase, ListarBilhetesPorRifaHandler>();
        services.AddScoped<IListarBilhetesPorStatusUseCase, ListarBilhetesPorStatusHandler>();
        services.AddScoped<IRegistrarCompraBilhetesUseCase, RegistrarCompraBilhetesHandler>();
        services.AddScoped<ICadastrarParticipanteUseCase, CadastrarParticipanteHandler>();
        services.AddScoped<IEditarParticipanteUseCase, EditarParticipanteHandler>();
        services.AddScoped<IGetParticipanteByIdUseCase, GetParticipanteByIdHandler>();
        services.AddScoped<IListarParticipantesPorRifaUseCase, ListarParticipantesPorRifaHandler>();
        services.AddScoped<IPesquisarParticipantesUseCase, PesquisarParticipantesHandler>();
        services.AddScoped<IGetUsuarioByIdUseCase, GetUsuarioByIdHandler>();
        services.AddScoped<ICadastrarRifaUseCase, CadastrarRifaUseCaseHandler>();
        services.AddScoped<IEditarRifaUseCase, EditarRifaHandler>();
        services.AddScoped<IEncerrarRifaUseCase, EncerrarRifaHandler>();
        services.AddScoped<IGetRifaByIdUseCase, GetRifaByIdHandler>();
        services.AddScoped<IListarRifasUseCase, ListarRifasHandler>();
        services.AddScoped<ISortearRifaUseCase, SortearRifaHandler>();
        services.AddScoped<ICadastrarUsuarioUseCase, CadastrarUsuarioHandler>();
        services.AddScoped<IEditarUsuarioUseCase, EditarUsuarioHandler>();
        services.AddScoped<IAtivarUsuarioUseCase, AtivarUsuarioHandler>();
        services.AddScoped<IDesativarUsuarioUseCase, DesativarUsuarioHandler>();
    }
}
