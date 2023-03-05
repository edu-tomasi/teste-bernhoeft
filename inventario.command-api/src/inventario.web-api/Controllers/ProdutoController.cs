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
            => Service = service;

        private IProdutoService Service { get; }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProdutoResponse>>> Get([FromQuery] FilterProdutoRequest request)
        {
            var result = await Service.ListarAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> GetById(Guid id)
        {
            var result = await Service.ListarAsync(new() { Id = id });
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> Post(ProdutoRequest request)
        {
            var result = await Service.AdicionarAsync(request);
            return CreatedAtAction(nameof(GetById), new { result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> Put(Guid id, ProdutoRequest request)
        {
            var response = await Service.AlterarAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Service.RemoverAsync(id);
            return NoContent();
        }
    }
}
