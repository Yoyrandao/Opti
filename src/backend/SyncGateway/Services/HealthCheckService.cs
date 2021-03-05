using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SyncGateway.Services
{
    public class HealthCheckService : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
                                                        CancellationToken cancellationToken =
                                                            new())
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}