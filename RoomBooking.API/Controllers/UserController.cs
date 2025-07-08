using AutoMapper;
using RoomBooking.Application.Communication;
using RoomBooking.Application.Interfaces;
using RoomBooking.Domain.DTOs.UserDTOs;
using RoomBooking.Domain.DTOs.ResourcesDTOs;
using RoomBooking.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RoomBooking.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(
            IMapper mapper,
            IUserService userService
        )
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        /// <summary>
        /// Obtém os lista de User a partir
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<List<UserDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("useres")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await this.userService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Inclui um novo User.
        /// </summary>
        /// <param name="userNovoDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponseBase<long>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] UserNovoDTO userNovoDTO)
        {
            var response = await this.userService.AddAsync(userNovoDTO);
            return Ok(response);
        }

        /// <summary>
        /// Obtém os dados de uma User a partir do ID
        /// </summary>
        [ProducesResponseType(typeof(ResponseBase<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await this.userService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        ///  Lista filtrada de Useres.
        /// </summary>
        /// <param name="userFiltroDTO">Define os parâmetros de paginação</param>
        /// <param name="paginationFilter">Define os filtros para busca</param>
        /// <returns>Retorna uma lista paginada de PadraoEmailItemListaDTO</returns>

        [ProducesResponseType(typeof(ResponseBase<PagedList<List<UserItemListaDTO>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("get-by-filter-async")]
        public async Task<IActionResult> GetByFilterAsync([FromQuery] PaginationFilter? paginationFilter, [FromQuery] UserFiltroDTO? userFiltroDTO)
        {
            var response = await this.userService.GetByFilterAsync(paginationFilter, userFiltroDTO);
            return Ok(response);
        }
    }
}
