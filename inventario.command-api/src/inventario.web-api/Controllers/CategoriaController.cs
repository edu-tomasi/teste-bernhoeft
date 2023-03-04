﻿using inventario.business.Models.Request;
using inventario.business.Models.Response;
using inventario.business.Service;
using Microsoft.AspNetCore.Mvc;

namespace inventario.web_api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase
    {
        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        public ICategoriaService _service { get; }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponse>> Get([FromQuery] Guid? Id, [FromQuery] string? Nome, [FromQuery] bool? Ativo)
        {
            var result = await _service.ListarAsync(Id, Nome, Ativo);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponse>> GetById(Guid Id)
        {
            var result = await _service.ListarAsync(Id);
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponse>> Post(CategoriaRequest request)
        {
            var result = await _service.AdicionarAsync(request);

            return CreatedAtAction(nameof(GetById), new { result.Id }, result);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaResponse>> Put(Guid Id, CategoriaRequest request)
        {
            var result = await _service.AlterarAsync(Id, request);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _service.RemoverAsync(Id);
            return NoContent();
        }
    }
}
