using Autoglass.Aplicacao.Interfaces;
using Autoglass.Aplicacao.RespostaController;
using Autoglass.Dominio.Entidades;

namespace Autoglass.Aplicacao.Services
{
    public class AutoglassService : IAutoglassService
    {
        private readonly IAutoglassRepository _repository;

        public AutoglassService(IAutoglassRepository repository)
        {
            _repository = repository;
        }

        public async Task<RespostaGenerica<List<Produto>>> RetornarProdutos(int tamanhoPagina, int numeroPagina, string? descricao = null,
            DateOnly? fabInicial = null, DateOnly? fabFinal = null, 
            DateOnly? valInicial = null, DateOnly? valFinal = null,
            int? fornecedorId = null)
        {
            var produtos = await _repository.RetornarProdutos();
            produtos = FiltrarLista(produtos, descricao, fabInicial, fabFinal, valInicial, valFinal, fornecedorId);
            produtos = PaginarLista(produtos, tamanhoPagina, numeroPagina);
            
            return new RespostaGenerica<List<Produto>>(produtos);
        }


        public async Task<RespostaGenerica<Produto>> InserirProduto(Produto produto)
        {
            var fornecedor = await _repository.RetornarFornecedorPorId(produto.FornecedorId);
            if (fornecedor == null)
                return new RespostaGenerica<Produto>("Código de fornecedor inserido é inválido.");
            await _repository.AdicionarProduto(produto);
            await _repository.SalvarAlteracoes();
            return new RespostaGenerica<Produto>(produto);
        }

        public async Task<RespostaGenerica<Produto>> EditarProduto(int id, Produto produto)
        {
            var produtoRetorno = await _repository.RetornarProdutoPorId(id);
            if (produtoRetorno == null)
                return new RespostaGenerica<Produto>();

            produtoRetorno.AlterarProduto(produto);
            _repository.SalvarAlteracoes();

            return new RespostaGenerica<Produto>(produtoRetorno);
        }

        public async Task<RespostaGenerica<Produto>> ExcluirProduto(int id)
        {
            var produtoRetorno = await _repository.RetornarProdutoPorId(id);
            if (produtoRetorno == null)
                return new RespostaGenerica<Produto>(produtoRetorno);
            produtoRetorno.ExcluirLogicamente();
            _repository.SalvarAlteracoes();
            return new RespostaGenerica<Produto>(produtoRetorno);
        }


        public async Task<RespostaGenerica<Produto>> RetornarProdutoPorId(int id)
        {
            var produto = await _repository.RetornarProdutoPorId(id);
            return new RespostaGenerica<Produto>(produto);
        }

        private List<Produto> FiltrarLista(List<Produto> produtos, string? descricao,
            DateOnly? fabInicial, DateOnly? fabFinal,
            DateOnly? valInicial, DateOnly? valFinal, int? fornecedorId)
        {
            if (descricao != null)
                produtos = produtos.Where(x => x.Descricao.Contains(descricao)).ToList();
            if (fabInicial != null)
                produtos = produtos.Where(x => x.DataFabricacao >= fabInicial).ToList();
            if (fabFinal != null)
                produtos = produtos.Where(x => x.DataFabricacao <= fabFinal).ToList();
            if (valInicial != null)
                produtos = produtos.Where(x => x.DataValidade >= valInicial).ToList();
            if (valFinal != null)
                produtos = produtos.Where(x => x.DataValidade <= valFinal).ToList();
            if (fornecedorId != null)
                produtos = produtos.Where(x => x.FornecedorId == fornecedorId).ToList();

            return produtos;
        }

        private List<Produto> PaginarLista(List<Produto> produtos, int tamanhoPagina, int numeroPagina)
        {
            var entradasRemovidas = tamanhoPagina * (numeroPagina - 1);
            produtos = produtos.Skip(entradasRemovidas).Take(tamanhoPagina).ToList();

            return produtos;
        }
    }
}
