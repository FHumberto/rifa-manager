using Microsoft.AspNetCore.Mvc;

namespace RifaManager.Api.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class BaseController : ControllerBase;
