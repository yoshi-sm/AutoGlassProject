using Autoglass.Aplicacao.Interfaces;
using Autoglass.Aplicacao.RespostaController;
using Autoglass.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Autoglass.Infra.Repository
{

    public class AutoglassRepository : IAutoglassRepository
    {
        private readonly IAutoglassContext _context;

        public AutoglassRepository(IAutoglassContext context)
        {
            _context = context;
        }

        public async Task<Produto> AdicionarProduto(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            return produto;
        }

        public async Task<Fornecedor?> RetornarFornecedorPorId(int id)
        {
            var produto = await _context.Fornecedores.Where(x => x.Id == id).FirstOrDefaultAsync();
            return produto;
        }

        public async Task<Produto?> RetornarProdutoPorId(int id)
        {
            var produto = await _context.Produtos.Include(x => x.Fornecedor).Where(x => x.Ativo && x.Id == id).FirstOrDefaultAsync();
            return produto;
        }

        public async Task<List<Produto>> RetornarProdutos()
        {
            var retorno =  await _context.Produtos.Include(x => x.Fornecedor).AsNoTracking().Where(x => x.Ativo).ToListAsync();
            return retorno;
        }

        public async void SalvarAlteracoes()
        {
            await _context.SaveChangesAsync();
        }
    }
}
