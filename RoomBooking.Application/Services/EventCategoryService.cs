using AutoMapper;
using BDC.Portal.Colaborador.Application.Communication;
using BDC.Portal.Colaborador.Application.Interfaces;
using BDC.Portal.Colaborador.Domain.DTOs.CategoriaEventoDTOs;
using BDC.Portal.Colaborador.Domain.Interfaces;



namespace BDC.Portal.Colaborador.Application.Services
{
    public class CategoriaEventoService : ServiceBase, ICategoriaEventoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMapper mapper;

        public CategoriaEventoService(IUnitOfWork unitOfWork, IMensagemRepository mensagemRepository, IMapper mapper) : base(mensagemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mensagemRepository = mensagemRepository;
            this.mapper = mapper;
        }

        public  async Task<ResponseBase<List<CategoriaEventoDTO>>> GetAllAsync()
        {
            var categoriaEvento = await unitOfWork.CategoriaEventoRepository.GetAllAsync();
            var categoriaEventoDTO = mapper.Map<List<CategoriaEventoDTO>>(categoriaEvento);
            
            return new ResponseBase<List<CategoriaEventoDTO>>(categoriaEventoDTO);
        }
    }
}
