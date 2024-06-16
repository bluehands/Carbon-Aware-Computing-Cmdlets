using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using CarbonAwareComputing.Cmdlets;
using CarbonAwareComputing;
using FunicularSwitch;

namespace CarbonAwareComputing.Cmdlets.Controller;

public class CarbonAwareExecutionTimeProcessor : CarbonAwareProcessor
{
    public CarbonAwareExecutionTimeProcessor() : base()
    {

    }

    public CarbonAwareExecutionTimeProcessor(WattTimeCredentials credentials) : base(credentials)
    {
    }

    public async Task<Result<DateTimeOffset>> Process(string location, DateTimeOffset earliestExecutionTime, DateTimeOffset latestExecutionTime, TimeSpan estimatedExecutionDuration, DateTimeOffset fallbackExecutionTime)
    {
        try
        {
            CarbonAwareDataProvider provider = DataProvider == DataProvider.OpenData ? CarbonAwareDataProvider : m_CarbonAwareDataProviderWattTime;
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
public class GridCarbonIntensityProcessor : CarbonAwareProcessor
{
    public GridCarbonIntensityProcessor() : base()
    {

    }

    public GridCarbonIntensityProcessor(WattTimeCredentials credentials) : base(credentials)
    {
    }

    public async Task<GridCarbonIntensity> Process(string location)
    {
        try
        {
            CarbonAwareDataProvider provider = DataProvider == DataProvider.OpenData ? CarbonAwareDataProvider : m_CarbonAwareDataProviderWattTime;
            return await provider.GetCarbonIntensity(new ComputingLocation(location), DateTimeOffset.Now);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return GridCarbonIntensity.NoData();
        }
    }
}

