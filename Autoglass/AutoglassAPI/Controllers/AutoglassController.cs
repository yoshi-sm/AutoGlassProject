using Autoglass.Aplicacao.Dto;
using Autoglass.Aplicacao.Interfaces;
using Autoglass.Dominio.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoglassAPI.Controllers
{

    //Geralmente se utiliza um service ou um Handler intermediando o controller e o service.
    //Como neste caso os métodos eram bem simples, decidi fazer a chamada direta ao service.
    [Route("api/[controller]")]
    [ApiController]
    public class AutoglassController : ControllerBase
    {
        private readonly IMapper _mapper;
        public AutoglassController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("RetornarProdutos")]
        public async Task<IActionResult> RetornarProdutos(
            [FromServices] IAutoglassService service,
            [FromQuery] string? descricao,
            [FromQuery] DateOnly? fabricacaoInicial,
            [FromQuery] DateOnly? fabricacaoFinal,
            [FromQuery] DateOnly? validadeInicial,
            [FromQuery] DateOnly? validadeFinal,
            [FromQuery] int? fornecedorId,
            [FromQuery] int tamanhoPagina = 3,
            [FromQuery] int numeroPagina = 1)
        {
            var resposta = await service.RetornarProdutos(tamanhoPagina, numeroPagina, descricao, fabricacaoInicial, 
                fabricacaoFinal, validadeInicial, validadeFinal, fornecedorId);
            if (!resposta.Objeto!.Any())
                return NotFound();
            return Ok(_mapper.Map<List<ProdutoDto>>(resposta.Objeto));
        }

        [HttpGet("RetornarProdutos/{id}")]
        public async Task<IActionResult> RetornarProdutoPorId([FromServices] IAutoglassService service, [FromRoute] int id)
        {
            var resposta = await service.RetornarProdutoPorId(id);
            if (resposta.Objeto == null) 
                return NotFound();
            return Ok(_mapper.Map<ProdutoDto>(resposta.Objeto));
        }

        [HttpPost("InserirProduto")]
        public async Task<IActionResult> CriarProduto([FromServices] IAutoglassService service, [FromBody] CriarProdutoDto dto)
        {
            var resposta = await service.InserirProduto(_mapper.Map<Produto>(dto));
            if(resposta.Objeto == null)
                return BadRequest(resposta.Mensagem);
            return Ok(_mapper.Map<ProdutoDto>(resposta.Objeto));
        }

        [HttpPut("AlterarProduto/{id}")]
        public async Task<IActionResult> EditarProduto([FromServices] IAutoglassService service, 
            [FromRoute] int id, [FromBody] EditarProdutoDto dto)
        {
            var resposta = await service.EditarProduto(id, _mapper.Map<Produto>(dto));
            if (resposta.Objeto == null)
                return NotFound();
            return Ok(_mapper.Map<ProdutoDto>(resposta.Objeto));
        }

        [HttpPut("remover/{id}")]
        public async Task<IActionResult> ExcluirProduto([FromServices] IAutoglassService service, [FromRoute] int id)
        {
            var resposta = await service.ExcluirProduto(id);
            if (resposta.Objeto == null)
                return NotFound();
            return Ok(_mapper.Map<ProdutoDto>(resposta.Objeto));
        }
    }
}
