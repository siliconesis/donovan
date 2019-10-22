using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Donovan;
using Donovan.Models;
using Donovan.Server;
using Donovan.Server.Services;

namespace Donovan.Server.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService service;

        public ManagersController(IManagerService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets the list of managers based on search criteria.
        /// </summary>
        /// <remarks>
        /// GET api/managers
        /// </remarks>
        /*
        [HttpGet]
        public IEnumerable<Manager> Get(string q)
        {
            System.Diagnostics.Debug.WriteLine(q);

            return new Manager[]
            {
                 new Manager { Email = "fred@bedrock.com", Name = "Fred Flintstone", Provider = "Microsoft" },
                 new Manager { Email = "wilma@bedrock.com", Name = "Wilma Flintstone", Provider = "Google" },
                 new Manager { Email = "barney@bedrock.com", Name = "Barney Rubble", Provider = "Facebook" },
                 new Manager { Email = "betty@bedrock.com", Name = "Betty Rubble", Provider = "Microsoft" }
            };
        }
        */

        /// <summary>
        /// Gets a manager.
        /// </summary>
        /// <remarks>
        /// GET api/managers/d2lsbWFAYmVkcm9jay5jb20=
        /// 
        /// Non-administrators can only retrieve their own information. Administrators
        /// can retrieve information for any manager.
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Manager))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await this.service.GetAsync(id));
        }

        /// <summary>
        /// Creates a manager.
        /// </summary>
        /// <remarks>
        /// POST api/managers
        /// </remarks>
        [HttpPost]
        public void Post([FromBody]Manager value)
        {
        }

        /// <summary>
        /// Updates a manager.
        /// </summary>
        /// <remarks>
        /// PUT api/managers/d2lsbWFAYmVkcm9jay5jb20=
        /// </remarks>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Manager value)
        {
        }

        /// <summary>
        /// Deletes a manager.
        /// </summary>
        /// <remarks>
        /// DELETE api/managers/d2lsbWFAYmVkcm9jay5jb20=
        /// </remarks>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            // TODO: Security: This should be accessible only to administrators.
        }
    }
}
