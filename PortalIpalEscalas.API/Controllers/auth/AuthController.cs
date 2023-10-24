using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using PortalIpalEscalas.Infraestructure.Interfaces;
using PortalIpalEscalas.Common.Models;

namespace PortalIpalEscalas.API.Controllers.auth
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {

        public AuthController() { 
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromServices] IAuthService authService, RegisterResponse request)
        {
            var result = await authService.UserRegister(request);
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost] 
        public async Task<ActionResult> Authentication([FromServices] IAuthService authService, [FromBody] AuthResponse authModel) {
            var result = await authService.AutheService(authModel);
            if(!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
