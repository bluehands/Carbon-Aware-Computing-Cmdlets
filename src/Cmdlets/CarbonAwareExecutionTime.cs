using System;
using System.Management.Automation;
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


    protected override void ProcessRecord()
    {
        base.ProcessRecord();
        var processor = new CarbonAwareExecutionTimeProcessor();
        processor.Process(Location, EarliestExecutionTime, LatestExecutionTime, EstimatedExecutionDuration, FallbackExecutionTime).GetAwaiter().GetResult()
            .Match(
                t => WriteObject(t),
                e => HandleError(e, default)
                );
    }
}
