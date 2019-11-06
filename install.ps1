Write-Host "Installing AWS CLI..." -ForegroundColor Cyan

Write-Host "Downloading..."
$msiPath = "$($env:USERPROFILE)\AWSCLI64.msi"
(New-Object Net.WebClient).DownloadFile('https://s3.amazonaws.com/aws-cli/AWSCLI64.msi', $msiPath)

Write-Host "Installing..."
cmd /c start /wait msiexec /i $msiPath /quiet
del $msiPath