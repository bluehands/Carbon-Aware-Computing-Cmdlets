using System;
using System.Threading.Tasks;
using CarbonAwareComputing.ExecutionForecast;
using FunicularSwitch;
using static CarbonAwareComputing.ExecutionForecast.ExecutionTime;

namespace CarbonAwareComputing.Cmdlets.Controller;

public class CarbonAwareExecutionTimeProcessor
{
    private static readonly CarbonAwareDataProviderOpenData s_CarbonAwareDataProvider = new CarbonAwareDataProviderOpenData();
    public async Task<Result<DateTimeOffset>> Process(string location, DateTimeOffset earliestExecutionTime, DateTimeOffset latestExecutionTime, TimeSpan estimatedExecutionDuration, DateTimeOffset fallbackExecutionTime)
    {
        try
        {
            var forecast = await s_CarbonAwareDataProvider.CalculateBestExecutionTime(new ComputingLocation(location), earliestExecutionTime, latestExecutionTime, estimatedExecutionDuration);
            return forecast.Match(
                _ => fallbackExecutionTime,
                bestExecutionTime => bestExecutionTime.ExecutionTime
            );
        }
        catch (Exception ex)
        {
            return Result.Error<DateTimeOffset>(ex.Message);
        }
    }
}
