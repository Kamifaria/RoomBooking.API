using AutoMapper;
using BDC.Portal.Colaborador.Application.Communication;
using BDC.Portal.Colaborador.Application.Interfaces;
using BDC.Portal.Colaborador.Domain.DTOs.EventoDTOs;
using BDC.Portal.Colaborador.Domain.Entities;
using BDC.Portal.Colaborador.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BDC.Portal.Colaborador.Application.Services
{
    public class EventoService : ServiceBase, IEventoService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMensagemRepository mensagemRepository;
        private readonly IMapper mapper;

        public EventoService(IUnitOfWork unitOfWork, IMensagemRepository mensagemRepository, IMapper mapper) : base(mensagemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mensagemRepository = mensagemRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseBase<int>> AddAsync(EventoNovoDTO eventoNovoDTO)
        {
            var eventoNovo = mapper.Map<EventoNovoDTO, Evento>(eventoNovoDTO);
           
            var colaboradores = unitOfWork.ColaboradorRepository.GetAll(c => eventoNovoDTO.ColaboradoresIds.Contains(c.Id)).ToList();
            if (!colaboradores.Any())
                throw new ValidationException(GetMessageByCodeMessage("MSG_LISTA_VAZIA", "Colaborador"));

            eventoNovo.Colaboradores = colaboradores;

            await unitOfWork.EventoRepository.AddAsync(eventoNovo);
           
            return new ResponseBase<int>(
                eventoNovo.Id,
                new Message(GetMessageByCodeMessage("MSG_CADASTRADO_SUCESSO", "Evento"))
            );
        }


        public async Task<ResponseBase<EventoEditadoDTO>> UpdateAsync(EventoEditadoDTO eventoEditadoDTO)
        {
            var eventoExistente = ValidateIfAlreadyExists(eventoEditadoDTO.Id);

            eventoExistente = mapper.Map(eventoEditadoDTO, eventoExistente);

            var colaboradores = unitOfWork.ColaboradorRepository.GetAll(c => eventoEditadoDTO.ColaboradoresIds.Contains(c.Id)).ToList();
            if (!colaboradores.Any())
                throw new ValidationException(GetMessageByCodeMessage("MSG_LISTA_VAZIA", "Colaborador"));
            eventoExistente.Colaboradores = colaboradores;

            var now = DateTime.Now;

            unitOfWork.EventoRepository.Update(eventoExistente);

            return await Task.FromResult(new ResponseBase<EventoEditadoDTO>(new Message(GetMessageByCodeMessage("MSG_ALTERADO_SUCESSO", "Evento"))));
        }

        public async Task<ResponseBase<EventoDTO>> GetByIdAsync(int id)
        {

            var evento = ValidateIfAlreadyExists(id);
            var eventoDTO = mapper.Map<Evento, EventoDTO>(evento);
            return await Task.FromResult(new ResponseBase<EventoDTO>(eventoDTO));
        }

        private Evento ValidateIfAlreadyExists(int id)
        {
            var includes = new List<Func<IQueryable<Evento>, IIncludableQueryable<Evento, object>>>
            {
                query => query
                .Include(evento => evento.Sala)
                .Include(evento => evento.Colaboradores)
                .Include(evento => evento.CriadoPor)
                .Include(evento => evento.TipoEvento)
                .Include(evento => evento.Recursos)
                .Include(evento => evento.CategoriaEvento)
            };

            var eventoExistente = unitOfWork.EventoRepository.Find(x => x.Id.Equals(id), includes);

            if (eventoExistente is null)
                throw new ValidationException(GetMessageByCodeMessage("MSG_REGISTROS_NAO_ENCONTRADOS","Evento"));
           
            return eventoExistente;
        }

        public async Task<ResponseBase<List<EventoDTO>>> GetAllAsync()
        {
            var includes = new List<Func<IQueryable<Evento>, IIncludableQueryable<Evento, object>>>
                {
                 query => query
                    .Include(e => e.Sala)
                    .Include(e => e.Colaboradores)
                    .Include(e => e.CriadoPor)
                    .Include(e => e.Recursos)
                    .Include(e => e.TipoEvento)
                    .Include(e => e.CategoriaEvento)
                };
            var eventos = await unitOfWork.EventoRepository.GetAllAsync(includes);
            var eventosDTO = mapper.Map<List<EventoDTO>>(eventos);

            return new ResponseBase<List<EventoDTO>>(eventosDTO);
        }

    }
}
