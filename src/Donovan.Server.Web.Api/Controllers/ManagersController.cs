using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Donovan.Game;
using Donovan.Server.Services;
using Donovan.Utilities;

namespace Donovan.Server.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService service;

        public ManagersController(IManagerService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets a manager.
        /// </summary>
        /// <remarks>
        /// GET api/managers/d2lsbWFAYmVkcm9jay5jb20=
        /// 
        /// Requires an authenticated user. Normal users can can only retrieve their own
        /// information. Administrators can retrieve information for any manager.
        /// </remarks>
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Manager))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Manager>> GetAsync(string email)
        {
            // TODO: Implement policy (claims?) to ensure only users (managers) can call this API.
            var manager = await this.service.GetAsync(Base64Helper.FromBase64(email));

            return Ok(manager);
        }

        /// <summary>
        /// Registers a manager.
        /// </summary>
        /// <remarks>
        /// POST api/managers
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Manager))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync([FromBody]RegistrationRequest request)
        {
            var response = await this.service.RegisterAsync(request);

            // TODO: Return 409 if manager already exists.
            // NOTE: Should this be a generic response? Is indicating an account already exists a security hole?
            return Ok(response);
        }

        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SigninResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SigninResponse>> SignInAsync([FromBody]SigninRequest request)
        {
            var response = await this.service.SignInAsync(request);

            if (response == null)
                return new StatusCodeResult(401);

            return Ok(response);
        }

        /// <summary>
        /// Updates a manager.
        /// </summary>
        /// <remarks>
        /// PUT api/managers/d2lsbWFAYmVkcm9jay5jb20=
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<ActionResult<Manager>> UpdateAsync(string id, [FromBody]Manager value)
        {
            return Ok(value);
        }

        /// <summary>
        /// Deletes a manager.
        /// </summary>
        /// <remarks>
        /// DELETE api/managers/d2lsbWFAYmVkcm9jay5jb20=
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            // TODO: Security: This should be accessible only to administrators.
            return Ok();
        }
    }
}
