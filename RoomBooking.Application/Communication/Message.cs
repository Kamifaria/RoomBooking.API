using BDC.Portal.Colaborador.Domain.Enums;

namespace BDC.Portal.Colaborador.Application.Communication
{
    public class Message
    {
        public string? Text { get; private set; }

        public MessageTypeEnum Type { get; private set; }

        public Message()
        {
            Text = string.Empty;
            Type = MessageTypeEnum.SUCCESS;
        }

        public Message(string text)
        {
            Text = text;
            Type = MessageTypeEnum.SUCCESS;
        }

        public Message(string text, MessageTypeEnum type)
        {
            Text = text;
            Type = type;
        }
    }
}
