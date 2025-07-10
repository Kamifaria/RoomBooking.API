using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC.Portal.Colaborador.Application.Communication
{
    public class ResponseBase<T>
    {
        public T? Data { get; private set; }

        public Message? Message { get; private set; }

        public ResponseBase(T data)
        {
            Data = data;
            Message = new Message();
        }

        public ResponseBase(Message message)
        {
            Message = message;
        }

        public ResponseBase(T data, Message message)
        {
            Data = data;
            Message = message;
        }
    }
}
