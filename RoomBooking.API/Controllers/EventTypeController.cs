using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.TipoReservationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace RoomBooking.API.Controllers
{
    [Route("api/TipoReservation")]
    [ApiController]
    public class TipoReservationController : ControllerBase
    {
        private readonly ITipoReservationService tipoReservationService;

        public TipoReservationController(ITipoReservationService tipoReservationService)
        {
            this.tipoReservationService = tipoReservationService;
        }

        /// <summary>
        /// Obtém os lista de TipoReservation a partir
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<List<TipoReservationDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("tipo-reservation")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await this.tipoReservationService.GetAllAsync();
            return Ok(response);
        }
    }
}
