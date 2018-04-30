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
bower install bootstrap#4.1.0 --save
Write-Output 'bootstrap installed.'

Write-Output 'installing jquery...'
bower install jquery#3.3.1 --save
Write-Output 'jquery installed.'

Write-Output 'installing popper...'
bower install popper.js#1.14.3 --save
Write-Output 'popper installed.'

if(!(Test-Path -Path ("$FolderPath\.gitignore"))){
	Write-Output "creating .gitignore file..."
    $Content = "
/node_modules
/npm-debug.log
/bower_components
bower.json
"
	Set-Content -Path ("$FolderPath\.gitignore") -Value $Content
}

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'