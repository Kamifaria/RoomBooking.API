using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.UserDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseBase<List<UserDTO>>> GetAllAsync();
        Task<ResponseBase<UserDTO>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(NewUserDTO user);
        Task<ResponseBase<UserDTO>> UpdateAsync(EditedUserDTO user);
    }
}
