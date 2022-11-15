using Grpc.Core;
using MediatR;
using MMGTS.Domain.Contracts.Repositories;
using MMGTS.Domain.Entities;
using MMGTS.Server.Commands;
using MMGTS.Server.Protos;
using MMGTS.SharedKernel.Utils;

namespace MMGTS.Server.Services
{
    public class MatchService : Protos.MatchService.MatchServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MatchService> _logger;

        public MatchService(
            IMediator mediator,
            ILogger<MatchService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<Match> CreateMatch(CreateMatchMessage message, ServerCallContext context)
        {
            _logger.LogInformation($"{context.Method} invoked");

            var cmd = new CreateMatchCommand
            {
                WhitePlayerId = message.WhitePlayerId,
                BlackPlayerId = message.BlackPlayerId,
                TimeControl = message.TimeControl
            };

            var id = await _mediator.Send(cmd);


            return new Match
            {
                Id = id,
                WhitePlayerId = message.WhitePlayerId,
                BlackPlayerId = message.BlackPlayerId,
                TimeControl = message.TimeControl
            };
        }

    }
}