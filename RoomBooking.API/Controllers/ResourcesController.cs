using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.ResourcesDTOs;
using Microsoft.AspNetCore.Mvc;

namespace RoomBooking.API.Controllers
{
    [Route("api/Resources")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService resourcesService;

        public ResourcesController(IResourcesService resourcesService)
        {
            this.resourcesService = resourcesService;
        }

        /// <summary>
        /// Obtém os lista de reservation a partir
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<List<ResourcesDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("resources")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await this.resourcesService.GetAllAsync();
            return Ok(response);
        }
    }
}
