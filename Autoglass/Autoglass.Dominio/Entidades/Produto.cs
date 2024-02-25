using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateOnly? DataFabricacao { get; set; }
        public DateOnly? DataValidade { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }
}
