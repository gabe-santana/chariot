using MediatR;
using MMGTS.Application.Notifications;
using MMGTS.Domain.Entities;
using MMGTS.Server.Commands;
using MMGTS.Server.Repositories;
using System.Text.RegularExpressions;

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
            try
            {
                var match = new MatchData
                {
                    Id = Guid.NewGuid(),
                    WPlayerId = request.WhitePlayerId,
                    BPlayerId = request.BlackPlayerId,
                    TimeControl = request.TimeControl,
                    Date = DateTime.UtcNow
                };

                await _genericRepo.Add(match);
                await _mediator.Publish(new CreatedMatchNotification { MatchId = match.Id.ToString(), WhitePlayerId = match.WPlayerId, BlackPlayerId = match.BPlayerId });

                return Guid.NewGuid().ToString();
            }
            catch (Exception ex) 
            {
                await _mediator.Publish(new ErrorNotification { Message = ex.Message, StackTrace = ex.StackTrace });
                return await Task.FromResult("Occured an error while creating a match");
            }
        }
    }
}
