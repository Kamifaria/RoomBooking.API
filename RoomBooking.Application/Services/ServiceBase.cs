using BDC.Portal.Colaborador.Domain.Entities;
using BDC.Portal.Colaborador.Domain.Interfaces;

namespace BDC.Portal.Colaborador.Application.Services
{
    public abstract class ServiceBase
    {
        private readonly IMensagemRepository mensagemRepository;

        public ServiceBase(IMensagemRepository MensagemRepository)
        {
            mensagemRepository = MensagemRepository;
        }

        public string GetMessageByCodeMessage(string codeMessage, params string[] parameters)
        {
            string codeMessage2 = codeMessage;
            Mensagem mensagem = mensagemRepository.Find((Mensagem msg) => msg.Codigo.Equals(codeMessage2));
            if (mensagem == null)
            {
                return "Erro Generico";
            }

            if (parameters != null && parameters.Any())
            {
                return string.Format(mensagem.Texto, parameters);
            }
            return mensagem.Texto;
        }
    }
}
