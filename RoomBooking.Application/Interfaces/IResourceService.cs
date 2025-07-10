using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.ResourceDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IResourceService
    {
        Task<ResponseBase<List<ResourceDTO>>> GetAllAsync();
        Task<ResponseBase<ResourceDTO>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(NewResourceDTO resource);
        Task<ResponseBase<ResourceDTO>> UpdateAsync(EditedResourceDTO resource);
    }
}
