using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace RifaManager.Api.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    protected Guid GetUsuarioAutenticadoId()
    {
        string? usuarioId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(usuarioId!);
    }
}
