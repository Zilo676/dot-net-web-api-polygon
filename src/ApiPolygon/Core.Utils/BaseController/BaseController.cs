using System.Net;
using Core.Utils.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Core.Utils.BaseController;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
public abstract class BaseController : ControllerBase
{
}