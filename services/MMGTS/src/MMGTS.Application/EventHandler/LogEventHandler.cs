using MediatR;
using Microsoft.Extensions.Logging;
using MMGTS.Application.Notifications;

namespace MMGTS.Application.EventHandler
{
    public class LogEventHandler :
                            INotificationHandler<CreatedMatchNotification>,
                            INotificationHandler<ErrorNotification>
    {

        private readonly ILogger<LogEventHandler> _logger;

        public LogEventHandler(ILogger<LogEventHandler> _logger)
        {
            this._logger = _logger;
        }
        public Task Handle(CreatedMatchNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation($"[Match Creation] MatchId: {notification.MatchId}, White: {notification.WhitePlayerId}, Black: {notification.BlackPlayerId}");
            });

        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation($"[Match Error] Message: {notification.Message}, StackTrace: {notification.StackTrace}");
            });
        }
    }
}

