using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_Tenant_API.Application.Interfaces;

namespace Multi_Tenant_API.API.Controllers
{

    [Controller]
    [Route("api/[controller]")]
    public class EntityAController : Controller
    {
        private readonly IEntityAService entityAService;
        public EntityAController(IEntityAService entityAService) {
            
            this.entityAService = entityAService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEntities() {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var students = await entityAService.GetEntities();
            return Ok(students);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, String name) {
            
            //update the entity with the id = id if its belong to the tenet that in the current resolved val
            return BadRequest(ModelState);
            
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, String name)
        {

            //update the entity with the id = id if its belong to the tenet that in the current resolved val
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(String name)
        {

            //update the entity with the id = id if its belong to the tenet that in the current resolved val
            return BadRequest(ModelState);

        }


    }
}
