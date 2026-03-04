using ApiColombia.BL.Interfaces;
using ApiColombia.Entities.DTO.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiColombia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // protegido con JWT
    public class RegionsController : ControllerBase
    {
        private readonly IApiColombiaServices _service;

        public RegionsController(IApiColombiaServices service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtener todas las regiones
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var regions = await _service.GetAllAsync(cancellationToken);
            return Ok(regions);
        }

        /// <summary>
        /// Obtener región por Id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var region = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(region);
        }

        /// <summary>
        /// Crear nueva región
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreateRegionDto dto,
            CancellationToken cancellationToken)
        {
            var id = await _service.CreateAsync(dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
        }

        /// <summary>
        /// Actualizar región
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateRegionDto dto,
            CancellationToken cancellationToken)
        {
            await _service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Eliminar región
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Sincronizar desde API externa
        /// </summary>
        [HttpPost("sync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Sync(CancellationToken cancellationToken)
        {
            await _service.SyncFromExternalAsync(cancellationToken);
            return NoContent();
        }
    }
}