using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_Tenant_API.Application.DTOs;
using Multi_Tenant_API.Application.Interfaces;
using Multi_Tenant_API.Application.Services;
using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.API.Controllers
{

    [Controller]
    [Route("api/[controller]")]
    public class EntityAController : Controller 
    {
        private readonly IEntityAService entityAService;
        private readonly ILogger<AuthService> logger;
        public EntityAController(IEntityAService entityAService, ILogger<AuthService> logger) {
            
            this.entityAService = entityAService;
            this.logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEntities() {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
                

            var students = await entityAService.GetEntities();
            return Ok(students);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , EntityCreationDto entityCreationDto) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try
            {

                bool isUpdated = await entityAService.UpdateEntity(entityCreationDto, id);
                if (!isUpdated)
                {
                    return NotFound();
                }

                return Ok(entityCreationDto);


            }
            catch (UnauthorizedAccessException ex) {

                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }


        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                bool isDeleted = await entityAService.DeleteEntity(id);
                if (!isDeleted) return NotFound();
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post( EntityCreationDto entityCreationDTO)
        {

            if (!ModelState.IsValid) {
                logger.LogError("Model State is Not Valid To Add Entity");
                return BadRequest(ModelState);
            }
                

            bool isAdded= await entityAService.AddEntity(entityCreationDTO);

            if (!isAdded) {
                logger.LogError("Entity Not Added");
                return NotFound();
            }

            logger.LogInformation("Entity Added");
            return Ok();

        }


    }
}
