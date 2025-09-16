using System.Net;
using Core.Data.DTO;
using Core.Utils.BaseService;
using Core.Utils.Pagination;
using Core.Utils.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Utils.BaseController;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
public class CrudController<TEntity, TDto> : BaseController where TDto : BaseDto
{
    protected readonly IBaseService<TEntity, TDto> _service;
    protected readonly ILogger<CrudController<TEntity, TDto>> _logger;

    public CrudController(IBaseService<TEntity, TDto> service,
        ILogger<CrudController<TEntity, TDto>> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet(nameof(Get))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> Get([FromQuery] QueryParameters.QueryParameters parameters)
    {
        _logger.LogInformation("GetAll");

        var (expression, pagination, order) = parameters.PrepareData<TDto>();
        var dtos = _service.GetAll(expression, pagination, order);
        RequestHeaderInjector.AddPaginationHeaders(Response, pagination);
        return Ok(dtos);
    }

    [HttpGet(nameof(Get) + "/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public virtual async Task<ActionResult<TDto>> Get(Guid id)
    {
        _logger.LogInformation(nameof(Get));

        var entityDto = await _service.GetByIdAsync(id);
        if (entityDto == null)
        {
            throw new ArgumentNullException($@"Entity with {id} not found");
        }

        return Ok(entityDto);
    }

    [HttpPost(nameof(Create))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TDto? dto)
    {
        _logger.LogInformation(nameof(Create));

        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var createdEntity = await _service.AddAsync(dto);
        return Ok(createdEntity);
    }

    [HttpPut(nameof(Update))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public virtual async Task<ActionResult<TDto>> Update([FromBody] TDto? dto)
    {
        _logger.LogInformation(nameof(Update));

        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var updatedEntity = await _service.UpdateAsync(dto);
        return Ok(updatedEntity);
    }

    [HttpDelete(nameof(Remove) + "/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public virtual async Task<ActionResult> Remove(Guid id)
    {
        _logger.LogInformation(nameof(Remove));

        await _service.DeleteAsync(id);
        return Ok();
    }
}