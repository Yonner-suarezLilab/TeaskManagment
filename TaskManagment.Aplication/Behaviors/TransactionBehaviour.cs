using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using TaskManagment.Aplicacion.Extensions;
using TaskManagment.Infraestructura;

namespace TaskManagment.Aplicacion.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

        public TransactionBehaviour(ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType().Name;

            try
            {
                // Puedes agregar lógica específica aquí antes de la ejecución del siguiente paso

                _logger.LogInformation("----- Begin processing {CommandName} ({@Command})", typeName, request);

                response = await next();

                _logger.LogInformation("----- Complete processing {CommandName}", typeName);

                // Puedes agregar lógica específica aquí después de la ejecución del siguiente paso

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling {CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
    }
    //


