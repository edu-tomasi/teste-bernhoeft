using inventario.business.Models.Request;
using inventario.business.Models.Response;
using inventario.business.Service;
using inventario.web_api.Models;
using inventario.web_api.Validators;
using Microsoft.AspNetCore.Mvc;

namespace inventario.web_api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase
    {
        public CategoriaController(ICategoriaService service, CategoriaValidator validator)
            => (Service, Validator) = (service, validator);

        private ICategoriaService Service { get; }
        private CategoriaValidator Validator { get; }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> Get(FilterCategoriaRequest request)
        {
            var result = await Service.ListarAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponse>> GetById(Guid id)
        {
            var result = await Service.ListarAsync(new() { Id = id });
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponse>> Post(CategoriaRequest request)
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
        public async Task<ActionResult<CategoriaResponse>> Put(Guid id, CategoriaRequest request)
        {
            var validate = await Validator.ValidateAsync(request);
            
            if (validate.IsValid)
            {
                var result = await Service.AlterarAsync(id, request);

                return Ok(result);
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
