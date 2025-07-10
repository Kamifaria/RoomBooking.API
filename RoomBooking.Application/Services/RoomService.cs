using AutoMapper;
using BDC.Portal.Colaborador.Application.Communication;
using BDC.Portal.Colaborador.Application.Interfaces;
using BDC.Portal.Colaborador.Domain.DTOs.SalaDTOs;
using BDC.Portal.Colaborador.Domain.Interfaces;


namespace BDC.Portal.Colaborador.Application.Services
{
    public class SalaService : ServiceBase, ISalaService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMapper mapper;

        public SalaService(IUnitOfWork unitOfWork, IMensagemRepository mensagemRepository, IMapper mapper) : base(mensagemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mensagemRepository = mensagemRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseBase<List<SalaDTO>>> GetAllAsync()
        {
            var sala = await unitOfWork.SalaRepository.GetAllAsync();
            var salaDTO = mapper.Map<List<SalaDTO>>(sala);
            return new ResponseBase<List<SalaDTO>>(salaDTO);
        }
    }
}
