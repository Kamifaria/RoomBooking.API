using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace RoomBooking.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica o user e disponibiliza token para acesso ao sistema.
        /// </summary>
        /// <param name="credentials">Informa o nome de user, senha e o tipo de acesso.</param>
        /// <returns>Retorna um token em string.</returns>
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("credentials")]
        public async Task<IActionResult> Credentials([FromBody] AuthDTO credentials)
        {
            var token = await _authService.GenerateJwtToken(credentials);
            return Ok(token);
        }
    }
}
