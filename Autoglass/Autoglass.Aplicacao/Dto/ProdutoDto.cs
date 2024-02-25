using Autoglass.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Dto
{
    public class ProdutoDto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateOnly? DataFabricacao { get; set; }
        public DateOnly? DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string FornecedorCNPJ { get; set; }
        public string DescricaoFornecedor { get; set; }
    }
}
