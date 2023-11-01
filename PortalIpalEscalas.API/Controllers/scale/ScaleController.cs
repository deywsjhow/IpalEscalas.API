using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalIpalEscalas.API.Controllers.scale
{
    [ApiController]
    [Route("scale/")]
    public class ScaleController : ControllerBase
    {
        private readonly IToken token;


        public ScaleController(IToken _token) { 
            this.token = _token;
        }

        [HttpPost]
        [Route("v1/registerscale")]
        //[Authorize("Bearer")]
        [ProducesResponseType(typeof(ObjectResponse<RegisterScaleResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResponse<RegisterScaleResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> RegisterScale([FromServices] IScaleService scaleService, [FromBody] RegisterScaleResponse request)
        {
            var result = await scaleService.ScaleRegister(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpPost]
        [Authorize("Bearer")]
        [Route("v1/userscale")]
        [ProducesResponseType(typeof(ObjectListResponse<SelectScalerForUserRequest>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectListResponse<SelectScalerForUserRequest>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SelectScaleForUser([FromServices] IScaleService scaleService, [FromBody] SelectScalerForUserRequest request)
        {
            var result = await scaleService.SelectScaleForUser(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }



        [HttpPost]
        [Route("v1/datescale")]
        //[Authorize("Bearer")]
        [ProducesResponseType(typeof(ObjectListResponse<SelectScalerForAnyDate>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectListResponse<SelectScalerForAnyDate>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SelectScaleForAnyDate([FromServices] IScaleService scaleService, [FromBody] SelectScalerForAnyDate request)
        {
            var result = await scaleService.SelectScaleForAnyDate(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
