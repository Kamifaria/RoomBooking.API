using AutoMapper;
using BDC.Portal.Colaborador.Application.Communication;
using BDC.Portal.Colaborador.Application.Interfaces;
using BDC.Portal.Colaborador.Domain.DTOs.TipoEventoDTOs;
using BDC.Portal.Colaborador.Domain.Interfaces;


namespace BDC.Portal.Colaborador.Application.Services
{
    public class TipoEventoService : ServiceBase, ITipoEventoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMapper mapper;

        public TipoEventoService(IUnitOfWork unitOfWork, IMensagemRepository mensagemRepository, IMapper mapper) : base(mensagemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mensagemRepository = mensagemRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseBase<List<TipoEventoDTO>>> GetAllAsync()
        {
            var tipoEvento = await unitOfWork.TipoEventoRepository.GetAllAsync();
            var tipoEventoDTO = mapper.Map<List<TipoEventoDTO>>(tipoEvento);

            return new ResponseBase<List<TipoEventoDTO>>(tipoEventoDTO);
        }
    }
}
