using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using CarbonAwareComputing.Cmdlets.Cmdlets;
using CarbonAwareComputing.ExecutionForecast;
using FunicularSwitch;
using static CarbonAwareComputing.ExecutionForecast.ExecutionTime;

namespace CarbonAwareComputing.Cmdlets.Controller;

public class CarbonAwareExecutionTimeProcessor
{
    private static readonly CarbonAwareDataProviderOpenData s_CarbonAwareDataProvider = new();
    private static readonly Dictionary<WattTimeCredentials, CarbonAwareDataProviderWattTime> s_CarbonAwareDataProviderWattTime = new();
    private readonly DataProvider m_DataProvider = DataProvider.OpenData;
    private readonly CarbonAwareDataProviderWattTime m_CarbonAwareDataProviderWattTime;

    public CarbonAwareExecutionTimeProcessor()
    {

    }

    public CarbonAwareExecutionTimeProcessor(WattTimeCredentials credentials)
    {
        m_DataProvider = DataProvider.WattTime;
        if (!s_CarbonAwareDataProviderWattTime.TryGetValue(credentials, out m_CarbonAwareDataProviderWattTime))
        {
            m_CarbonAwareDataProviderWattTime = new CarbonAwareDataProviderWattTime(credentials.UserName, credentials.Password);
            s_CarbonAwareDataProviderWattTime[credentials] = m_CarbonAwareDataProviderWattTime;
        }
    }

    public async Task<Result<DateTimeOffset>> Process(string location, DateTimeOffset earliestExecutionTime, DateTimeOffset latestExecutionTime, TimeSpan estimatedExecutionDuration, DateTimeOffset fallbackExecutionTime)
    {
        try
        {
            CarbonAwareDataProvider provider = m_DataProvider == DataProvider.OpenData ? s_CarbonAwareDataProvider : m_CarbonAwareDataProviderWattTime;
            var forecast = await provider.CalculateBestExecutionTime(new ComputingLocation(location), earliestExecutionTime, latestExecutionTime, estimatedExecutionDuration);
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

public record WattTimeCredentials(string UserName, string Password);
