# Good to know:
# Select all properties of an object: (Get-Item -Path ".\") | Select-Object -Property *
# git: 
# 	git clone https://github.com/brainy301/templates-and-scripts.git
#	git add *
#	git status
#	git config --global user.name ""
#	git config --global user.name
#	git config --global user.email ""
#	git commit -a -m "Some message"
#	git remote -v
#	git config credential.helper store
#	git push origin master
#	git pull
#	dotnet new webapi -lang C#

# Config:
$FolderName = (Get-Item -Path ".\").Name
$FolderPath = (Get-Item -Path ".\").FullName

# Create bower file
$ProjectName = $FolderName.ToLower()
$Content = "
{ 
	""name"": ""$($ProjectName)"", 
	""description"": """", 
	""main"": """", 
	""license"": ""MIT"", 
	""homepage"": """", 
	""private"": true,
	""ignore"": [
		""**/.*"",
		""node_modules"",
		""bower_components"",
		""test"",
		""tests""
	]
}
"
Set-Content -Path ("$FolderPath\bower.json") -Value $Content
Write-Output 'bower file created'

# Install dependencies
Write-Output 'installing fontawesome...'
bower install fontawesome#4.7.0 --save
Write-Output 'fontawesome installed.'

Write-Output 'installing bootstrap...'
bower install bootstrap#4.1.0
Write-Output 'bootstrap installed.'

Write-Output 'installing jquery...'
bower install jquery#3.3.1
Write-Output 'jquery installed.'

Write-Output 'downloading site icon...'
(New-Object System.Net.WebClient).DownloadFile("http://www.iconeasy.com/icon/ico/Application/iPhonica%20Vol.%202/Notes.ico", ("$FolderPath\images\favicon.ico"))
Write-Output 'site icon downloaded.'

# Install templates
Write-Output 'downloading default.html...'
Write-Output $("$FolderPath\default.html")
(New-Object System.Net.WebClient).DownloadFile("https://raw.githubusercontent.com/brainy301/templates-and-scripts/master/front%20ends/bootstrap-fontawesome/default.html", $("$FolderPath\default.html"))
Write-Output 'default.html downloaded.'

Write-Output 'creating folders...'
if(!(Test-Path -Path ("$FolderPath\scripts"))){
	Write-Output "creating scripts folder..."
    New-Item -ItemType directory -Path ("$FolderPath\scripts")
}
if(!(Test-Path -Path ("$FolderPath\images"))){
	Write-Output "creating images folder..."
    New-Item -ItemType directory -Path ("$FolderPath\images")
}
if(!(Test-Path -Path ("$FolderPath\styles"))){
	Write-Output "creating styles folder..."
    New-Item -ItemType directory -Path ("$FolderPath\styles")
}
Write-Output 'folders created.'

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'