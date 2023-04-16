# Carbon Aware Computing PowerShell Cmdlets

## Overview

A PowerShell Cmdlet to get the best execution time for a tasks with **carbon awareness** in mind. The best point in time is calculated based on emission forecasts to get a window with a minimal grid carbon intensity.

## Installation

CarbonAwareComputing.Cmdlets is available via PowerShell Gallery. 

``` powershell
Install-Module -Name CarbonAwareComputing
```

## Usage

Use the *Get-CarbonAwareExecutionTime* to get a forecast for the best execution time. If no forecast is available the fallback time will use (*FallbackExecutionTime*-Parameter).

## Methodology

**CarbonAwareComputing** Cmdlets make use of the [Carbon Aware SDK](https://github.com/Green-Software-Foundation/carbon-aware-sdk) a [Green Software Foundation](https://greensoftware.foundation/) Project. There are some extensions to the SDK to use cached offline data sources in our [fork](https://github.com/bluehands/carbon-aware-sdk).

The emission forecast data are uploaded periodically to a Azure Blob Storage for a given grid region and are public (e.g. for Germany <https://carbonawarecomputing.blob.core.windows.net/forecasts/de.json>).

For the list of supported locations check the [Carbon Aware Computing GitHub Repository](https://github.com/bluehands/Carbon-Aware-Computing)