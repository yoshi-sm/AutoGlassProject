using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.RespostaController
{
    public class RespostaGenerica<T>
    {
        public string? Mensagem { get; set; }
        public T? Objeto { get; }

        public RespostaGenerica(string? mensagem)
        {
            Mensagem = mensagem;
        }

        public RespostaGenerica(string? mensagem, T? objeto)
        {
            Mensagem = mensagem;
            Objeto = objeto;
        }

        public RespostaGenerica(T? objeto)
        {
            Objeto = objeto;
        }
    }
}
