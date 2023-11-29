using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using PortalIpalEscalas.Infraestructure.Interfaces;
using PortalIpalEscalas.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PortalIpalEscalas.Common.Models.Utils;

namespace PortalIpalEscalas.API.Controllers.auth
{
    [ApiController]
    [Route("auth/")]
    public class AuthController : ControllerBase
    {       

        [HttpPost]
        [Route("v1/register")]
        [ProducesResponseType(typeof(ObjectResponse<RegisterResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResponse<RegisterResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromServices] IAuthService authService, [FromBody] RegisterResponse request)
        {
            var result = await authService.UserRegister(request);
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        [Route("v1/login")]
        [ProducesResponseType(typeof(ObjectResponse<AuthResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResponse<AuthResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Authentication([FromServices] IAuthService authService, [FromBody] Login authModel) {
            var result = await authService.AutheService(authModel);
            if(!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/changepassword")]
        [ProducesResponseType(typeof(ObjectResponse<ChangePass>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResponse<ChangePass>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangePassword([FromServices] IAuthService authService, [FromBody] ChangePass changePass)
        {
            var result = await authService.ChangePassword(changePass);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/getusers")]
        [ProducesResponseType(typeof(ObjectListResponse<UserLogin>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectListResponse<UserLogin>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUsers([FromServices] IAuthService authService)
        {
            var result = await authService.GetUsers();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost]
        [Route("v1/sendmessage")]
        [ProducesResponseType(typeof(ObjectResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SendMessageWpp([FromServices] IAuthService authService, SendMessageWpp request)
        {
            var result = await authService.SendMessageWpp(request);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


    }
}
