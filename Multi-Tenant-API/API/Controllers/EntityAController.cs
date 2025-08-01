using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_Tenant_API.Application.DTOs;
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
        public async Task<IActionResult> Update([FromRoute] int id , EntityCreationDto entityCreationDto) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isUpdated = await entityAService.UpdateEntity(entityCreationDto,id);
            if (!isUpdated)
                return NotFound();

            return Ok(entityCreationDto);

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isDeleted= await entityAService.DeleteEntity(id);
            if (!isDeleted)
                return NotFound();

            return Ok();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post( EntityCreationDto entityCreationDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isAdded= await entityAService.AddEntity(entityCreationDTO);
            
            if (!isAdded)
                return NotFound();

            return Ok();

        }


    }
}
