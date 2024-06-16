# Publish NuGet Package

## Publish to PowerShell Gallery

```powershell
$env:NUGET_CLI_LANGUAGE="en-us"
$apiKey=""
publish-Module  -Name CarbonAwareComputing -NuGetApiKey $apiKey
```