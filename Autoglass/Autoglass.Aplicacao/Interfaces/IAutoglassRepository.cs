using Autoglass.Aplicacao.Dto;
using Autoglass.Aplicacao.RespostaController;
using Autoglass.Dominio.Entidades;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Interfaces
{
    public interface IAutoglassRepository
    {
        public Task<List<Produto>> RetornarProdutos();
        public Task<Produto?> RetornarProdutoPorId(int id);
        public Task<Produto> AdicionarProduto(Produto produto);
        public Task<Fornecedor?> RetornarFornecedorPorId(int id);
        public void SalvarAlteracoes();
    }


}
