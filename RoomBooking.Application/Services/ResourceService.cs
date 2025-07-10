using AutoMapper;
using BDC.Portal.Colaborador.Application.Communication;
using BDC.Portal.Colaborador.Application.Interfaces;
using BDC.Portal.Colaborador.Domain.DTOs.RecursosDTOs;
using BDC.Portal.Colaborador.Domain.Interfaces;


namespace BDC.Portal.Colaborador.Application.Services
{
    public class RecursosService : ServiceBase, IRecursosService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMapper mapper;

        public RecursosService(IUnitOfWork unitOfWork, IMensagemRepository mensagemRepository, IMapper mapper) : base(mensagemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mensagemRepository = mensagemRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseBase<List<RecursosDTO>>> GetAllAsync()
        {
            var recursos = await unitOfWork.RecursosRepository.GetAllAsync();
            var recursosDTO = mapper.Map<List<RecursosDTO>>(recursos);

            return new ResponseBase<List<RecursosDTO>>(recursosDTO);
        }
    }
}
