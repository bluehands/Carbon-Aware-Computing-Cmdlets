using System;
using System.Management.Automation;
using System.Net;
using System.Security;
using CarbonAwareComputing.Cmdlets.Controller;
using CarbonAwareComputing.Cmdlets.Models;

namespace CarbonAwareComputing.Cmdlets.Cmdlets;

[Cmdlet(Verbs.Get, Nouns.CarbonAwareExecutionTime)]
public class CarbonAwareExecutionTime : CmdletBase
{
    [Parameter(Mandatory = true)]
    public string Location { get; set; }
    [Parameter(Mandatory = true)]
    public DateTimeOffset EarliestExecutionTime { get; set; }
    [Parameter(Mandatory = true)]
    public DateTimeOffset LatestExecutionTime { get; set; }
    [Parameter(Mandatory = true)]
    public TimeSpan EstimatedExecutionDuration { get; set; }
    [Parameter(Mandatory = false)]
    public DateTimeOffset FallbackExecutionTime { get; set; }
    [Parameter(Mandatory = false)]
    public DataProvider Provider { get; set; }
    [Parameter(Mandatory = false)]
    public string WattTimeUsername { get; set; }
    [Parameter(Mandatory = false)]
    public SecureString WattTimePassword { get; set; }

    protected override void ProcessRecord()
    {
        base.ProcessRecord();
        var processor = Provider == DataProvider.OpenData ?
            new CarbonAwareExecutionTimeProcessor() :
            new CarbonAwareExecutionTimeProcessor(new WattTimeCredentials(WattTimeUsername, ConvertFromSecureString(WattTimePassword)));
        processor.Process(Location, EarliestExecutionTime, LatestExecutionTime, EstimatedExecutionDuration, FallbackExecutionTime).GetAwaiter().GetResult()
            .Match(
                t => WriteObject(t),
                e => HandleError(e, default)
                );
    }

    private string ConvertFromSecureString(SecureString secureString)
    {
        var c = new NetworkCredential(string.Empty, secureString);
        return c.Password;
    }
}

public enum DataProvider
{
    OpenData,
    WattTime
}
