using Grpc.Core;
using Protos;

namespace BF.Api.Services;
public class SpecService : Protos.SpecService.SpecServiceBase
{
    private readonly ILogger<SpecService> _logger;
    public SpecService(ILogger<SpecService> logger) => _logger = logger;

    public override Task<SpecResponse> Spec(SpecRequest request, ServerCallContext context)
     => Task.FromResult(new SpecResponse { Status = 200 });

    public override Task<AboutResponse> About(AboutRequest request, ServerCallContext context)
     => Task.FromResult(new AboutResponse { Author = "Per", Detail = "test" });
}