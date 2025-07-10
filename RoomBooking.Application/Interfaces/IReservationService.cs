using RoomBooking.Application.Common;
using RoomBooking.Domain.DTOs.ReservationDTOs;

namespace RoomBooking.Application.Interfaces
{
    public interface IReservationService
    {
        Task<ResponseBase<List<ReservationDTO>>> GetAllAsync();
        Task<ResponseBase<ReservationDTO>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(NewReservationDTO reservation);
        Task<ResponseBase<ReservationDTO>> UpdateAsync(EditedReservationDTO reservation);
    }
}
