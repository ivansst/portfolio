using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Portfolio.Server.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize(AuthenticationSchemes = "Bearer")]
  public abstract class ApiController : ControllerBase
  {
    protected string UserId => User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
  }
}
