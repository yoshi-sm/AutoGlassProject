using Autoglass.Dominio;
using Autoglass.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Dominio
{
    [TestFixture]
    public class ProdutoTestes
    {
        [Test]
        public void AlterarProduto_DeveAlterarObjeto()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));
            var produtoFinal = new Produto("falso", DateOnly.FromDateTime(DateTime.Now.AddYears(2)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)));

            //Act
            produto.AlterarProduto(produtoFinal);

            //Assert
            Assert.That(produto.Descricao.Equals(produtoFinal.Descricao) 
                && produto.DataFabricacao == produtoFinal.DataFabricacao 
                && produto.DataValidade == produtoFinal.DataValidade);
        }

        [Test]
        public void ExcluirLogicamente_DeverAlterarAtivoParaFalso()
        {
            //Arrange
            var produto = new Produto("algo", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));

            //Act
            produto.ExcluirLogicamente();

            //Assert
            Assert.That(!produto.Ativo);
        }

    }
}
