using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Dominio.Entidades
{
    public class Fornecedor
    {
        public int Id {  get; private set; }
        public string? Descricao { get; private set; }
        public string? CNPJ { get; private set;}
    }
}
