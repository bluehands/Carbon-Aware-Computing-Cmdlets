# Carbon Aware Computing PowerShell Cmdlets

## Overview

A set of PowerShell Cmdlet for **carbon awareness** in mind. Get the best execution time for a tasks with minimal grid carbon intenity. The best point in time is calculated based on emission forecasts to get a window with a minimal grid carbon intensity. 
Additinaly get the actual grid carbon intensity.

## Installation

CarbonAwareComputing.Cmdlets is available via PowerShell Gallery.

``` powershell
Install-Module -Name CarbonAwareComputing
```

## Usage

Use the *Get-CarbonAwareExecutionTime* to get a forecast for the best execution time. If no forecast is available the fallback time will use (*FallbackExecutionTime*-Parameter).

``` powershell
$now = get-date
Get-CarbonAwareExecutionTime -Location de -EarliestExecutionTime $now -LatestExecutionTime ($now).AddHours(10) -EstimatedExecutionDuration "00:10:00" 
```
Use the *Get-GridCarbonIntensity* to get the actual grid carbon intensity.

``` powershell
get-GridCarbonIntensity -Location "fr" -Provider OpenData 
```

## Methodology

**CarbonAwareComputing** Cmdlets make use of the [Carbon Aware SDK](https://github.com/Green-Software-Foundation/carbon-aware-sdk) a [Green Software Foundation](https://greensoftware.foundation/) Project. There are some extensions to the SDK to use cached offline data sources in our [fork](https://github.com/bluehands/carbon-aware-sdk).

The emission forecast data are uploaded periodically to a Azure Blob Storage for a given grid region and are public (e.g. for Germany <https://carbonawarecomputing.blob.core.windows.net/forecasts/de.json>).

For the list of supported locations check the [Carbon Aware Computing GitHub Repository](https://github.com/bluehands/Carbon-Aware-Computing)

## Fallback

For regions currently not supported with open data you may use the build in WattTime client. For that you must provide a valid WattTime Account.

``` powershell
# Forecast for Western Australia

$now = get-date
$userName = Read-Host
$pwd = Read-Host -AsSecureString

Get-CarbonAwareExecutionTime -Location wem -EarliestExecutionTime $now -LatestExecutionTime ($now).AddHours(10) -EstimatedExecutionDuration "00:10:00" -Provider WattTime -WattTimeUsername $userName -WattTimePassword $pwd
```