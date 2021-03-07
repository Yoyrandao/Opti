using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SyncGateway.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
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