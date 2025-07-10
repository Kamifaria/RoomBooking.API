using BDC.Portal.Colaborador.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using ColaboradorEntity = BDC.Portal.Colaborador.Domain.Entities.Colaborador;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BDC.Portal.Colaborador.Application.Communication;
using AutoMapper;
using BDC.Portal.Colaborador.Domain.Interfaces;
using BDC.Portal.Colaborador.Exceptions.ApplicationExceptions;
using BDC.Portal.Colaborador.Domain.DTOs.Auth;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace BDC.Portal.Colaborador.Application.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMensagemRepository mensagemRepository;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IMensagemRepository mensagemRepository) : base(mensagemRepository)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.mensagemRepository = mensagemRepository;
        }

        public async Task<ResponseBase<string>> GenerateJwtToken(AuthDTO login)
        {
            var colaboradorEntity = AuthenticateUser(login);
         
            if (colaboradorEntity == null)
                return new ResponseBase<string>(login.Email, new Message(GetMessageByCodeMessage("MSG_REGISTROS_NAO_ENCONTRADOS_LOGIN")));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, colaboradorEntity.Nome), // Identificação do usuário
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único do token
                new Claim(JwtRegisteredClaimNames.Email, colaboradorEntity.Pessoal.Email), // E-mail
                new Claim("id", colaboradorEntity.Id.ToString()), // ID do usuário
                new Claim("nome", colaboradorEntity.Nome), // Nome do usuário
                new Claim("role", colaboradorEntity.Perfil.Nome) // role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Expira em 1 hora
                signingCredentials: creds
            );

            return await Task.FromResult(new ResponseBase<string>(new JwtSecurityTokenHandler().WriteToken(token)));
        }


        private ColaboradorEntity? AuthenticateUser(AuthDTO login)
        {
            var includes = new List<Func<IQueryable<ColaboradorEntity>, IIncludableQueryable<ColaboradorEntity, object>>>
            {
                query => query
                .Include(colaborador => colaborador.Pessoal)
                .Include(colaborador => colaborador.Perfil)
                .Include(colaborador => colaborador.Corporativo)
                .Include(colaborador => colaborador.Departamento)
                .Include(colaborador => colaborador.Login)
            };

            var colaboradorEntity = unitOfWork.ColaboradorRepository.Find(a=>a.Login.Senha == login.Password  && a.Login.Email  == login.Email, includes) ??
                throw new ValidationException(GetMessageByCodeMessage("MSG_REGISTROS_NAO_ENCONTRADOS" ,"Colaborador"));

            return colaboradorEntity;
        }
    }
}