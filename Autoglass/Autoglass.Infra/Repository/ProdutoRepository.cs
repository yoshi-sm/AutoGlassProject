using Autoglass.Aplicacao.Interfaces;
using Autoglass.Aplicacao.RespostaController;
using Autoglass.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Autoglass.Infra.Repository
{

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IAutoglassContext _context;

        public ProdutoRepository(IAutoglassContext context)
        {
            _context = context;
        }

        public async Task<RespostaGenerica<Produto>> Create(Produto produto)
        {
            var fornecedor = await _context.Fornecedores.AsNoTracking().Where(x => x.Id == produto.FornecedorId).FirstOrDefaultAsync();
            if (fornecedor == null) return new RespostaGenerica<Produto>("Código do fornecedor inválido");

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return new RespostaGenerica<Produto>(produto);
        }

        public async Task<int> ExclusaoLogica(int id)
        {
            var possivelProduto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id && x.Ativo);
            if (possivelProduto == null) return 0;

            possivelProduto.Ativo = false;
            var retorno = await _context.SaveChangesAsync();
            return retorno;
        }

        public async Task<List<Produto>> GetAll()
        {
            var produtos = await _context.Produtos.AsNoTracking().Include(x => x.Fornecedor).Where(x => x.Ativo).ToListAsync();
            return produtos;
        }

        public async Task<Produto?> GetById(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().Include(x => x.Fornecedor).Where(x => x.Id == id && x.Ativo).FirstOrDefaultAsync();

            return produto;
        }

        public async Task<Produto?> Update(int id, Produto produto)
        {
            var possivelProduto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id && x.Ativo);
            if (possivelProduto == null) return 0;

            possivelProduto.DataFabricacao = produto.DataFabricacao;
            possivelProduto.DataValidade = produto.DataValidade;
            possivelProduto.Descricao = produto.Descricao;
            
            var resultado = await _context.SaveChangesAsync();
            return resultado;
        }
    }
}
