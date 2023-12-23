using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Shared;

namespace TaskManagment.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        private ObjectResult RetornarErrorGenerico()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, GenericoResponse.Error());
        }

        [NonAction]
        public ObjectResult Return201(object result)
        {
            if (result == null)
            {
                return RetornarErrorGenerico();
            }
            return StatusCode(StatusCodes.Status201Created, GenericoResponse.Ok(StatusCodes.Status201Created, data: result));
        }

        [NonAction]
        public ObjectResult Return200(object result)
        {
            if (result == null)
            {
                return RetornarErrorGenerico();
            }
            return StatusCode(StatusCodes.Status200OK, GenericoResponse.Ok(data: result));
        }

        [NonAction]
        public ObjectResult Return204(object result)
        {
            if (result == null)
            {
                return RetornarErrorGenerico();
            }
            return StatusCode(StatusCodes.Status200OK, GenericoResponse.Ok(resultado: StatusCodes.Status204NoContent, data: result));
        }

        [NonAction]
        public ObjectResult Return400(string messages)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                GenericoResponse.Error(result: StatusCodes.Status400BadRequest, message: messages));
        }

    }

}
