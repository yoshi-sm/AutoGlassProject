using Autoglass.Aplicacao.Interfaces;
using Autoglass.Aplicacao.Services;
using Autoglass.Dominio.Entidades;
using Moq;

namespace Testes.Applicacao
{
    [TestFixture]
    public class AutoglassServiceTestes
    {
        private Mock<IAutoglassRepository> _repo;
        private AutoglassService _service;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IAutoglassRepository>();
            _service = new AutoglassService(_repo.Object);
        }

        [Test]
        public async Task InserirProduto_FornecedorInexistente_RetornaStringDeErro()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            produto.FornecedorId = 5;
            _repo.Setup(r => r.RetornarFornecedorPorId(5)).ReturnsAsync(value: null);

            //Act
            var resposta = await _service.InserirProduto(produto);

            //Assert
            Assert.That(!string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Mensagem!.Equals("Código de fornecedor inserido é inválido."));
        }

        [Test]
        public async Task InserirProduto_ProdutoInserido_RetornarRespostaGenericaComProduto()
        {
            //Arrange
            var fornecedor = new Fornecedor();
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            produto.FornecedorId = 5;
            produto.Fornecedor = fornecedor;
            _repo.Setup(r => r.RetornarFornecedorPorId(5)).ReturnsAsync(fornecedor);

            //Act
            var resposta = await _service.InserirProduto(produto);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto!.Equals(produto));
        }

        [Test]
        public async Task EditarProduto_ProdutoInexistente_RetornarRespostaGenericaComObjetoNulo()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            _repo.Setup(r => r.RetornarProdutoPorId(5)).ReturnsAsync(value: null);

            //Act
            var resposta = await _service.EditarProduto(5, produto);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto == null);
        }

        [Test]
        public async Task EditarProduto_ProdutoAlterado_RetornarProdutoAlterado()
        {
            //Arrange
            var produtoQuery = new Produto("teste", DateOnly.Parse("11/02/2025"), DateOnly.Parse("11/02/2026"));
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            _repo.Setup(r => r.RetornarProdutoPorId(5)).ReturnsAsync(produtoQuery);

            //Act
            var resposta = await _service.EditarProduto(5, produto);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto!.Descricao == produto.Descricao 
                && resposta.Objeto.DataFabricacao == produto.DataFabricacao
                && resposta.Objeto.DataValidade == produto.DataValidade);
        }

        [Test]
        public async Task ExcluirProduto_ProdutoInexistente_RetornarObjetoNulo()
        {
            //Arrange
            _repo.Setup(r => r.RetornarProdutoPorId(5)).ReturnsAsync(value: null);

            //Act
            var resposta = await _service.ExcluirProduto(5);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto == null);
        }

        [Test]
        public async Task ExcluirProduto_ProdutoExcluido_RetornarProdutoComAtivoFalso()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            _repo.Setup(r => r.RetornarProdutoPorId(5)).ReturnsAsync(produto);

            //Act
            var resposta = await _service.ExcluirProduto(5);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto != null && !resposta.Objeto.Ativo);
        }
        

        [Test]
        public async Task RetornarProdutoPorId_ProdutoInexistente_RetornarObjetoNulo()
        {
            //Arrange
            _repo.Setup(r => r.RetornarProdutoPorId(5)).ReturnsAsync(value: null);

            //Act
            var resposta = await _service.RetornarProdutoPorId(5);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto == null);
        }
        [Test]
        public async Task ExcluirProduto_ProdutoEncontrado_RetornarProduto()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            _repo.Setup(r => r.RetornarProdutoPorId(5)).ReturnsAsync(produto);

            //Act
            var resposta = await _service.RetornarProdutoPorId(5);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto == produto);
        }

        [Test]
        public async Task RetornarProdutos_FiltrosCorretos_RetornarProdutosFiltrados()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026")) {FornecedorId = 1 };
            var produtoFiltrado = new Produto("brbam", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026")) { FornecedorId = 1 };
            var produtoFiltradoDois = new Produto("algo", DateOnly.Parse("22/01/2025"), DateOnly.Parse("22/02/2026")) { FornecedorId = 1 };
            var produtoFiltradoTres = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/01/2026")) { FornecedorId = 1 };
            var produtoFiltradoQuatro = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026")) { FornecedorId = 2 };
            _repo.Setup(r => r.RetornarProdutos()).ReturnsAsync(new List<Produto>()
            {
                produto, produto, produto, produto, produtoFiltrado, produtoFiltradoDois, produtoFiltradoTres, produtoFiltradoQuatro
            });

            //Act
            var resposta = await _service.RetornarProdutos(5, 1, "algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("23/02/2025"),
                DateOnly.Parse("22/02/2026"), DateOnly.Parse("23/02/2026"), 1);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto.Count == 4);
        }

        [Test]
        public async Task RetornarProdutos_PaginacaoCorreta_RetornarProdutosDeAcordoComPaginacao()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.Parse("22/02/2025"), DateOnly.Parse("22/02/2026"));
            _repo.Setup(r => r.RetornarProdutos()).ReturnsAsync(new List<Produto>()
            {
                produto, produto, produto, produto, produto, produto
            });

            //Act
            var resposta = await _service.RetornarProdutos(5, 2);

            //Assert
            Assert.That(string.IsNullOrEmpty(resposta.Mensagem));
            Assert.That(resposta.Objeto.Count == 1);
        }

    }
}
