using Autoglass.Dominio.Entidades;

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
