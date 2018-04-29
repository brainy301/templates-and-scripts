# Config:
$FolderName = (Get-Item -Path ".\").Name
$FolderPath = (Get-Item -Path ".\").FullName

Remove-Item -Path ("$FolderPath\bower.json")
rm -r ("$FolderPath\bower_components")

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'