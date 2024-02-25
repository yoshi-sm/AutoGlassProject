namespace Autoglass.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; } = true;
        public DateOnly? DataFabricacao { get; private set; }
        public DateOnly? DataValidade { get; private set; }

        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        public Produto(string descricao, DateOnly? dataFabricacao, DateOnly? dataValidade)
        {
            Descricao = descricao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
        }

        public void AlterarProduto(Produto produto)
        {
            Descricao = produto.Descricao;
            DataFabricacao = produto.DataFabricacao;
            DataValidade = produto.DataValidade;
        } 

        public void ExcluirLogicamente()
        {
            Ativo = false;
        }
    }
}
