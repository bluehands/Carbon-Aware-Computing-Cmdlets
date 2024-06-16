using System.Collections.Generic;

namespace CarbonAwareComputing.Cmdlets.Controller;

public abstract class CarbonAwareProcessor
{
    protected static readonly CarbonAwareDataProviderOpenData CarbonAwareDataProvider = new();
    protected static readonly Dictionary<WattTimeCredentials, CarbonAwareDataProviderWattTime> CarbonAwareDataProviderWattTime = new();
    protected readonly DataProvider DataProvider = DataProvider.OpenData;
    protected readonly CarbonAwareDataProviderWattTime m_CarbonAwareDataProviderWattTime;

    protected CarbonAwareProcessor()
    {

    }

    protected CarbonAwareProcessor(WattTimeCredentials credentials)
    {
        DataProvider = DataProvider.WattTime;
        if (!CarbonAwareDataProviderWattTime.TryGetValue(credentials, out m_CarbonAwareDataProviderWattTime))
        {
            m_CarbonAwareDataProviderWattTime = new CarbonAwareDataProviderWattTime(credentials.UserName, credentials.Password);
            CarbonAwareDataProviderWattTime[credentials] = m_CarbonAwareDataProviderWattTime;
        }
    }
}
public record WattTimeCredentials(string UserName, string Password);