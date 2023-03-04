using inventario.business.Models.Request;
using inventario.business.Models.Response;
using inventario.business.Service;
using Microsoft.AspNetCore.Mvc;

namespace inventario.web_api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        private IProdutoService _service { get; }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProdutoResponse>> Get([FromQuery] Guid? Id, [FromQuery] string? Nome, [FromQuery] string? Descricao, [FromQuery] string? Categoria, [FromQuery] bool? Ativo)
        {
            throw new NotImplementedException();
            return Ok();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> GetById(Guid Id)
        {
            var result = await _service.ListarAsync(Id);
            
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> Post(ProdutoRequest request)
        {
            var result = await _service.AdicionarAsync(request);

            return CreatedAtAction(nameof(GetById), new { result.Id }, result);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoriaResponse> Put(Guid Id, CategoriaRequest request)
        {
            throw new NotImplementedException();
            return Ok();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            throw new NotImplementedException();
            return NoContent();
        }
    }
}
