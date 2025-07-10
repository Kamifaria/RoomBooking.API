using AutoMapper;
using BDC.Portal.Colaborador.Application.Communication;
using BDC.Portal.Colaborador.Application.Interfaces;
using BDC.Portal.Colaborador.Domain.DTOs.ColaboradorDTOs;
using BDC.Portal.Colaborador.Domain.Entities;
using BDC.Portal.Colaborador.Domain.Enums;
using BDC.Portal.Colaborador.Domain.Interfaces;
using BDC.Portal.Colaborador.Domain.Pagination;
using BDC.Portal.Colaborador.Exceptions.ApplicationExceptions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using ColaboradorEntity = BDC.Portal.Colaborador.Domain.Entities.Colaborador;
using BDC.Portal.Colaborador.Domain.DTOs.EventoDTOs;


namespace BDC.Portal.Colaborador.Application.Services
{
    public class ColaboradorService : ServiceBase, IColaboradorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMapper mapper;

        public ColaboradorService(IUnitOfWork unitOfWork, IMensagemRepository mensagemRepository, IMapper mapper) : base(mensagemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mensagemRepository = mensagemRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseBase<int>> AddAsync(ColaboradorNovoDTO colaboradorNovoDTO)
        {
            var colaboradorNovo = mapper.Map<ColaboradorNovoDTO, ColaboradorEntity>(colaboradorNovoDTO);

            await unitOfWork.ColaboradorRepository.AddAsync(colaboradorNovo);

            return new ResponseBase<int>(colaboradorNovo.Id, new Message(GetMessageByCodeMessage("MSG_CADASTRADO_SUCESSO", "Colaborador")));
        }

        public async Task<ResponseBase<List<ColaboradorDTO>>> GetAllAsync()
        {
            var includes = new List<Func<IQueryable<ColaboradorEntity>, IIncludableQueryable<ColaboradorEntity, object>>>
                {
                 query => query
                    .Include(e => e.Perfil)
                };
            var eventos = await unitOfWork.ColaboradorRepository.GetAllAsync(includes);
            var eventosDTO = mapper.Map<List<ColaboradorDTO>>(eventos);

            return new ResponseBase<List<ColaboradorDTO>>(eventosDTO);
        }

        public async Task<ResponseBase<PagedList<List<ColaboradorItemListaDTO>>>> GetByFilterAsync(PaginationFilter? paginationFilter, ColaboradorFiltroDTO? colaboradorFiltroDTO)
        {
            var veiculoPagedList = await unitOfWork.ColaboradorRepository.GetByFilterAsync(paginationFilter, colaboradorFiltroDTO);

            var data = !veiculoPagedList.PageContent.Any() ? null : veiculoPagedList;
            var messageText = (!veiculoPagedList.PageContent.Any() ? GetMessageByCodeMessage("MSG_REGISTROS_NAO_ENCONTRADOS") : string.Empty);
            var messageType = (!veiculoPagedList.PageContent.Any() ? MessageTypeEnum.WARNING : MessageTypeEnum.SUCCESS);

            return new ResponseBase<PagedList<List<ColaboradorItemListaDTO>>>(data, new Message(messageText, messageType));
        }

        public async Task<ResponseBase<ColaboradorDTO>> GetByIdAsync(int id)
        {
            var includes = new List<Func<IQueryable<ColaboradorEntity>, IIncludableQueryable<ColaboradorEntity, object>>>
            {
                query => query
                .Include(colaborador => colaborador.Pessoal)
                .Include(colaborador => colaborador.Perfil)
                .Include(colaborador => colaborador.Corporativo)
                .Include(colaborador => colaborador.Departamento)
                .Include(colaborador => colaborador.Login)
                .Include(colaborador => colaborador.Equipamentos)
            };

            var colaborador = unitOfWork.ColaboradorRepository.Find(x => x.Id.Equals(id), includes);
            
            var colaboradorDTO = mapper.Map<ColaboradorEntity, ColaboradorDTO>(colaborador);

            return await Task.FromResult(new ResponseBase<ColaboradorDTO>(colaboradorDTO));
        }

        public async Task<ResponseBase<ColaboradorDTO>> UpdateAsync(ColaboradorEditadoDTO colaboradorEditadoDTO)
        {
            var colaboradorExistente = ValidateIfAlreadyExists(colaboradorEditadoDTO.Id);

            colaboradorExistente = mapper.Map(colaboradorEditadoDTO, colaboradorExistente);

            var now = DateTime.Now;

            unitOfWork.ColaboradorRepository.Update(colaboradorExistente);

            return await Task.FromResult(new ResponseBase<ColaboradorDTO>(new Message(GetMessageByCodeMessage("MSG_ALTERADO_SUCESSO", "Colaborador"))));
        }


        private ColaboradorEntity ValidateIfAlreadyExists(int id)
        {
            var colaboradorExistente = unitOfWork.ColaboradorRepository.Find(x => x.Id.Equals(id));

            if (colaboradorExistente is null)
                throw new ValidationException(GetMessageByCodeMessage("MSG_REGISTROS_NAO_ENCONTRADOS"));
            return colaboradorExistente;
        }

        private void ValidateIfExistsInDatabase(int id)
        {
            var veiculoExistente = unitOfWork.ColaboradorRepository.Find(x => x.Id.Equals(id));

            if (veiculoExistente is not null)
                throw new ValidationException(GetMessageByCodeMessage("MSG_VIOLACAO_UNIQUE", $"com o código Colaborador {id}", "Colaborador"));
        }

    }

}
