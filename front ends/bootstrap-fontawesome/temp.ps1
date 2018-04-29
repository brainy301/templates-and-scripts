$FolderName = (Get-Item -Path ".\").Name
$FolderPath = (Get-Item -Path ".\").FullName

if(!(Test-Path -Path ("$FolderPath\scripts"))){
	Write-Output "Creating scripts folder..."
    New-Item -ItemType directory -Path ("$FolderPath\scripts")
}
if(!(Test-Path -Path ("$FolderPath\images"))){
	Write-Output "Creating images folder..."
    New-Item -ItemType directory -Path ("$FolderPath\images")
}
if(!(Test-Path -Path ("$FolderPath\styles"))){
	Write-Output "Creating styles folder..."
    New-Item -ItemType directory -Path ("$FolderPath\styles")
}

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'