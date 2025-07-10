using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.RoomDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IRoomService
    {
        Task<ResponseBase<List<RoomDTO>>> GetAllAsync();
        Task<ResponseBase<RoomDTO>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(NewRoomDTO room);
        Task<ResponseBase<RoomDTO>> UpdateAsync(EditedRoomDTO room);
    }
}
