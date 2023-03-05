using inventario.business.Models.Request;
using inventario.business.Models.Response;
using inventario.business.Service;
using inventario.web_api.Validators;
using Microsoft.AspNetCore.Mvc;

namespace inventario.web_api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        public ProdutoController(IProdutoService service, ProdutoValidator validator)
            => (Service, Validator) = (service, validator);

        private IProdutoService Service { get; }
        private ProdutoValidator Validator { get; }

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
            var validate = await Validator.ValidateAsync(request);

            if (validate.IsValid)
            {
                var result = await Service.AdicionarAsync(request);
                return CreatedAtAction(nameof(GetById), new { result.Id }, result);
            }

            var errorMessage = validate.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(errorMessage);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoResponse>> Put(Guid id, ProdutoRequest request)
        {
            var validate = await Validator.ValidateAsync(request);

            if (validate.IsValid)
            {
                var response = await Service.AlterarAsync(id, request);
                return Ok(response);
            }

            var errorMessage = validate.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(errorMessage);
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
