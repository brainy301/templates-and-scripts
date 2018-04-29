# To test, go to http://localhost:5014/api/Values
dotnet new webapi -lang C#
dotnet  build

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'
