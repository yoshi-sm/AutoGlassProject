using Autoglass.Aplicacao.RespostaController;
using Autoglass.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Interfaces
{
    public interface IAutoglassService
    {
        public Task<RespostaGenerica<List<Produto>>> RetornarProdutos(int tamanhoPagina, int numeroPagina, string? descricao, 
            DateOnly? fabInicial, DateOnly? fabFinal, DateOnly? valInicial, DateOnly? valFinal, int? fornecedorId);

        public Task<RespostaGenerica<Produto>> RetornarProdutoPorId(int id);
        public Task<RespostaGenerica<Produto>> InserirProduto(Produto produto);
        public Task<RespostaGenerica<Produto>> EditarProduto(int id, Produto produto);
        public Task<RespostaGenerica<Produto>> ExcluirProduto(int id);
    }
}
