using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.RoomDTOs;
using Microsoft.AspNetCore.Mvc;

namespace RoomBooking.API.Controllers
{
    [Route("api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        /// <summary>
        /// Obtém os lista de Room a partir
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<List<RoomDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("room")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await this.roomService.GetAllAsync();
            return Ok(response);
        }
    }
}
