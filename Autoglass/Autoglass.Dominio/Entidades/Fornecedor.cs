using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Dominio.Entidades
{
    public class Fornecedor
    {
        public int Id {  get; set; }
        public string? Descricao { get; set; }
        public string? CNPJ { get; set;}
    }
}
