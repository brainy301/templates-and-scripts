Write-Output 'Installing static server'
npm install -g node-static

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'