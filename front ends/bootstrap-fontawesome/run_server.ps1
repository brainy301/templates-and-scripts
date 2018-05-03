Write-Output 'Running static server'
static -p 8091

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'