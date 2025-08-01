using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Application.Services;
using Multi_Tenant_API.Domain;
using Multi_Tenant_API.Infrastructure.Data;

namespace Multi_Tenant_API.API.Controllers
{
   

    [Controller]
    [Route("api/auth/[controller]")]
    public class AuthController : Controller
    {
        private readonly IEntityAService entityAService;
        private readonly TokenProvider tokenProvider;

        public AuthController(IEntityAService entityAService, TokenProvider tokenProvider)
        {

            this.entityAService = entityAService;
            this.tokenProvider = tokenProvider;

        }


        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromHeader(Name = "ApiKey")] string key)
        {


            if (string.IsNullOrWhiteSpace(key))
            {

                return BadRequest("ERROR");
            }

            var id = await this.entityAService.Auth(key);
            if (id == null) {

                return BadRequest("Not found");
            }

            var token = tokenProvider.Create((int)id);


            return Ok(new { token });

        
        }
    }
}
