using Grpc.Core;
using MediatR;
using MMGTS.Server.Commands;
using MMGTS.SharedKernel.Utils;

namespace MMGTS.Server.Services
{
    public class DaprService : Protos.Dapr.DaprBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DaprService> _logger;

        public DaprService(
            IMediator mediator,
            ILogger<DaprService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<Protos.InvokeServiceResponseEnvelope> InvokeService(
            Protos.InvokeServiceEnvelope request,
            ServerCallContext context)
        {
            _logger.LogInformation($"Id: {request.Id}, method: {request.Method}");

            var responseEnvelope = new Protos.InvokeServiceResponseEnvelope();

            switch (request.Method)
            {

                case "CreateMatch":
                    {
                        var queryRequest = request.Data.ConvertFromAnyTypeAsync<CreateMatchCommand>();
                        var result = await _mediator.Send(queryRequest);
                        responseEnvelope.Data = result.ConvertToAnyTypeAsync();
                        return responseEnvelope;
                    }
            }

            return responseEnvelope;
        }

    }
}