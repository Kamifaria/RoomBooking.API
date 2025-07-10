using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.AuthDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseBase<AuthResponseDTO>> AuthenticateAsync(AuthRequestDTO credentials);
    }
}
