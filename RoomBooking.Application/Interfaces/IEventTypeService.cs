using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.EventTypeDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IEventTypeService
    {
        Task<ResponseBase<List<EventTypeDTO>>> GetAllAsync();
        Task<ResponseBase<EventTypeDTO>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(NewEventTypeDTO type);
        Task<ResponseBase<EventTypeDTO>> UpdateAsync(EditedEventTypeDTO type);
    }
}
