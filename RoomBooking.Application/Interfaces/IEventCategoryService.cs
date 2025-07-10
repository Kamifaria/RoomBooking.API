using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.EventCategoryDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IEventCategoryService
    {
        Task<ResponseBase<List<EventCategoryDTO>>> GetAllAsync();
        Task<ResponseBase<EventCategoryDTO>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(NewEventCategoryDTO category);
        Task<ResponseBase<EventCategoryDTO>> UpdateAsync(EditedEventCategoryDTO category);
    }
}
