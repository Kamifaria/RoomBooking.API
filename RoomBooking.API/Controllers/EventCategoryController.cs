using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.CategoriaReservationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace RoomBooking.API.Controllers
{
    [Route("api/CategoriaReservation")]
    [ApiController]
    public class CategoriaReservationController : ControllerBase
    {

        private readonly ICategoriaReservationService categoriaReservationService;

        public CategoriaReservationController(ICategoriaReservationService categoriaReservationService)
        {
            this.categoriaReservationService = categoriaReservationService;
        }
        /// <summary>
        /// Obtém os lista de reservation a partir
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<List<CategoriaReservationDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("categoria-reservation")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await this.categoriaReservationService.GetAllAsync();
            return Ok(response);
        }
    }
}
