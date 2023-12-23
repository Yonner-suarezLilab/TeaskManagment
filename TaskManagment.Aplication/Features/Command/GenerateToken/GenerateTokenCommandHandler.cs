using MediatR;
using TaskManagment.Infrastructure.Services.Token;
using TaskManagment.Shared.Models.Responses;

namespace TaskManagment.Aplication.Features.Command.GenerateToken
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenGenerated>
    {
        private readonly ITokenService _tokenService;

        public GenerateTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenGenerated> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _tokenService.GenerateToken();

            return new TokenGenerated
            {
                Token = token
            };
        }
    }
}
