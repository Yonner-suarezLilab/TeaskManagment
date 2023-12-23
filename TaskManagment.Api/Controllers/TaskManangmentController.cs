using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Aplicacion.Extensions;
using TaskManagment.Aplication.Features.Command.AddTasks;
using TaskManagment.Aplication.Features.Query.GetTasks;

namespace TaskManagment.Api.Controllers
{
    public class TaskManangmentController : ApiController
    {
        private readonly ILogger<TaskManangmentController>_logger;

        public TaskManangmentController(ILogger<TaskManangmentController> logger)
        {
            _logger = logger;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Task([FromBody] AddTaskCommand command)
        {

            _logger.LogInformation(
                "----- Sending commanad: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command),
                command,
                command);
            var commandResult = await Mediator.Send(command);
            return Return201(commandResult);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Tasks()
        {
            var query = new GetTasksQuery();
            _logger.LogInformation(
                "----- Sending commanad: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                query.GetGenericTypeName(),
                nameof(query),
                query,
                query);
            var commandResult = await Mediator.Send(query);
            return Return200(commandResult);
        }
    }
}
