using Autoglass.Aplicacao.Dto;
using Autoglass.Aplicacao.Interfaces;
using Autoglass.Dominio.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoglassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ProdutosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IProdutoRepository repository)
        {
            var produtos = await repository.GetAll();
            if (produtos.Any())
                return Ok(_mapper.Map<List<ProdutoDto>>(produtos));
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromServices] IProdutoRepository repository,[FromRoute] int id)
        {
            var produto = await repository.GetById(id);
            if (produto != null) {
                return Ok(_mapper.Map<ProdutoDto>(produto));
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] IProdutoRepository repository, [FromBody] CriarProdutoDto dto)
        {
            var resultado = await repository.Create(_mapper.Map<Produto>(dto));
            if(resultado.Mensagem == null)
                return Ok(resultado.Objeto);
            return BadRequest(resultado.Mensagem);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromServices] IProdutoRepository repository, [FromRoute] int id, [FromBody] EditarProdutoDto dto)
        {
            var resultado = await repository.Update(id, _mapper.Map<Produto>(dto));
            if (resultado == null)
                return Ok(resultado);
            return NotFound();
        }

        [HttpPut]
        [Route("remover/{id}")]
        public async Task<IActionResult> ExclusaoLogica([FromServices] IProdutoRepository repository, [FromRoute] int id)
        {
            var resultado = await repository.ExclusaoLogica(id);
            if (resultado > 0)
                return Ok();
            return NotFound();
        }
    }
}
