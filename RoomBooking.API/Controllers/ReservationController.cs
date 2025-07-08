using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.ReservationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace RoomBooking.API.Controllers
{
    [Route("api/Reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }


        /// <summary>
        /// Inclui um novo Reservation.
        /// </summary>
        /// <param name="reservationNovoDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseBase<long>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ReservationNovoDTO reservationNovoDTO)
        {
            var response = await this.reservationService.AddAsync(reservationNovoDTO);
            return Ok(response);
        }

        /// <summary>
        /// Obtém os dados de uma reservation a partir do ID
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<ReservationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await this.reservationService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Obtém os lista de reservation a partir
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<List<ReservationDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("reservations")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await this.reservationService.GetAllAsync();
            return Ok(response);
        }


        /// <summary>
        /// Inclui um novo Reservation.
        /// </summary>
        /// <param name="reservationEditadoDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseBase<ReservationEditadoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ReservationEditadoDTO reservationEditadoDTO)
        {
            var response = await this.reservationService.UpdateAsync(reservationEditadoDTO);
            return Ok(response);
        }



    }
}
