using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Donovan.Server.Security;
using Donovan.Server.Services;

namespace Donovan.Server.Web.Api.Controllers
{
    // TODO: The authentication results returned need to be more detailed to support
    //       monitoring, troubleshooting, and application responsiveness. Note that
    //       this work should be deferred until IdentityServer4 is integrated.

    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService service;

        public ClientsController(IClientService service)
        {
            this.service = service;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticationResponse>> LogInAsync([FromBody]AuthenticationRequest request)
        {
            var response = await this.service.AuthenticateAsync(request);

            if (response == null)
                return new StatusCodeResult(401);

            return Ok(response);
        }
    }
}
