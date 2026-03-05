using ApiColombia.BL.Exceptions;
using ApiColombia.Entities.DTO.CRUD;
using ApiColombia.Entities.Entities;
using ApiColombia.Services.Interfaces;
using ApiColombia.BL.Services.Interfaces;
using ApiColombia.DAL.Repository.ApiColombia.Interfaces;

/// <summary>
/// Servicio que gestiona la lógica de negocio relacionada con las regiones.
/// Incluye operaciones CRUD y sincronización con la API externa.
/// </summary>
public class ApiColombiaServices : IApiColombiaServices
{
    private readonly IApiColombiaRepository _repository;
    private readonly IExternalRegionService _externalService;

    public ApiColombiaServices(
        IApiColombiaRepository repository,
        IExternalRegionService externalService)
    {
        // Validación de dependencias obligatorias
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _externalService = externalService ?? throw new ArgumentNullException(nameof(externalService));
    }

    /// <summary>
    /// Obtiene todas las regiones de la base de datos.
    /// </summary>
    public async Task<IEnumerable<RegionDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var regions = await _repository.GetAllAsync();

        return regions.Select(r => new RegionDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description
        });
    }

    /// <summary>
    /// Obtiene una región por su Id.
    /// Lanza <see cref="ValidationException"/> si el Id es inválido.
    /// Lanza <see cref="NotFoundException"/> si no existe la región.
    /// </summary>
    public async Task<RegionDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ValidationException("Id must be greater than zero.");

        var region = await _repository.GetByIdAsync(id);

        if (region is null)
            throw new NotFoundException("Region not found.");

        return new RegionDto
        {
            Id = region.Id,
            Name = region.Name,
            Description = region.Description
        };
    }

    /// <summary>
    /// Crea una nueva región en la base de datos.
    /// Lanza <see cref="ValidationException"/> si los datos son inválidos.
    /// Lanza <see cref="ConflictException"/> si ya existe una región con el mismo nombre.
    /// </summary>
    public async Task<int> CreateAsync(
        CreateRegionDto dto,
        CancellationToken cancellationToken = default)
    {
        if (dto is null)
            throw new ValidationException("Request body cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ValidationException("Name is required.");

        var existingRegions = await _repository.GetAllAsync();

        if (existingRegions.Any(r =>
            r.Name.ToLower().Trim() == dto.Name.ToLower().Trim()))
        {
            throw new ConflictException("A region with the same name already exists.");
        }

        var region = new Region
        {
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim()
        };

        await _repository.AddAsync(region);

        return region.Id;
    }

    /// <summary>
    /// Actualiza una región existente.
    /// Lanza <see cref="ValidationException"/> si los datos son inválidos.
    /// Lanza <see cref="NotFoundException"/> si la región no existe.
    /// </summary>
    public async Task UpdateAsync(
        int id,
        UpdateRegionDto dto,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ValidationException("Invalid Id.");

        if (dto is null)
            throw new ValidationException("Request body cannot be null.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ValidationException("Name is required.");

        var region = await _repository.GetByIdAsync(id);

        if (region is null)
            throw new NotFoundException("Region not found.");

        region.Name = dto.Name.Trim();
        region.Description = dto.Description?.Trim();

        await _repository.UpdateAsync(region);
    }

    /// <summary>
    /// Elimina una región existente.
    /// Lanza <see cref="ValidationException"/> si el Id es inválido.
    /// Lanza <see cref="NotFoundException"/> si la región no existe.
    /// </summary>
    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ValidationException("Invalid Id.");

        var region = await _repository.GetByIdAsync(id);

        if (region is null)
            throw new NotFoundException("Region not found.");

        await _repository.DeleteAsync(region);
    }

    /// <summary>
    /// Sincroniza las regiones desde la API externa.
    /// Actualiza registros existentes o agrega nuevos.
    /// Evita guardar datos inválidos (sin nombre).
    /// </summary>
    public async Task SyncFromExternalAsync(
        CancellationToken cancellationToken = default)
    {
        var externalRegions = await _externalService.GetRegionsAsync(cancellationToken);

        if (externalRegions is null || !externalRegions.Any())
            return;

        var existingRegions = await _repository.GetAllAsync();

        var existingByExternalId = existingRegions
            .Where(r => r.ExternalId.HasValue)
            .ToDictionary(r => r.ExternalId!.Value);

        foreach (var ext in externalRegions)
        {
            if (string.IsNullOrWhiteSpace(ext.Name))
                continue;

            if (existingByExternalId.TryGetValue(ext.Id, out var existing))
            {
                if (existing.Name != ext.Name ||
                    existing.Description != ext.Description)
                {
                    existing.Name = ext.Name.Trim();
                    existing.Description = ext.Description?.Trim();

                    await _repository.UpdateAsync(existing);
                }

                continue;
            }

            var region = new Region
            {
                ExternalId = ext.Id,
                Name = ext.Name.Trim(),
                Description = ext.Description?.Trim()
            };

            await _repository.AddAsync(region);
        }
    }
}