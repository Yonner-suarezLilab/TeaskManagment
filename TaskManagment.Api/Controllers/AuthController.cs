using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagment.Aplicacion.Extensions;
using TaskManagment.Aplication.Features.Command.GenerateToken;

namespace TaskManagment.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GenerateToken()
        {
            var command = new GenerateTokenCommand();

            _logger.LogInformation(
                "----- Sending commanad: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command),
                command,
                command);
            var commandResult = await Mediator.Send(command);
            return Return201(commandResult);
        }
    }
}
