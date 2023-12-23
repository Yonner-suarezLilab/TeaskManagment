using MediatR;
using TaskManagment.Shared.Models.Responses;

namespace TaskManagment.Aplication.Features.Command.GenerateToken
{
    public class GenerateTokenCommand : IRequest<TokenGenerated>
    {
        public GenerateTokenCommand()
        {
        }
    }
}
