$powershellModulePath = [IO.Path]::Combine([Environment]::GetFolderPath("MyDocuments"), "Powershell", "Modules", "CarbonAwareComputing")
[system.io.directory]::CreateDirectory($powershellModulePath)
$fromPattern = [IO.Path]::Combine($PSScriptRoot,"*")
Copy-Item -Path "$fromPattern"  -Destination "$powershellModulePath" -Force
