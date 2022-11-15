using MediatR;
using MMGTS.Application.Notifications;
using MMGTS.Domain.Contracts.Repositories;
using MMGTS.Domain.Entities;
using MMGTS.Server.Commands;

namespace MMGTS.Server.Handlers
{
    public class CreateMatchHandler
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepo<MatchData> _genericRepo;

        public CreateMatchHandler(IMediator mediator, IGenericRepo<MatchData> genericRepo)
        {
            _mediator = mediator;
            _genericRepo = genericRepo;
        }

        public async Task<string> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var matchId = Guid.NewGuid();
            try
            {
                var match = new MatchData
                {
                    Id = matchId,
                    WPlayerId = request.WhitePlayerId,
                    BPlayerId = request.BlackPlayerId,
                    TimeControl = request.TimeControl,
                    Date = DateTime.UtcNow
                };

                await _genericRepo.Add(match);
                await _mediator.Publish(new CreatedMatchNotification { MatchId = match.Id.ToString(), WhitePlayerId = match.WPlayerId, BlackPlayerId = match.BPlayerId });

                return $"Match created {matchId}";
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { Message = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult($"Occured an error while creating a match: {ex.InnerException}");
            }
        }
    }
}
