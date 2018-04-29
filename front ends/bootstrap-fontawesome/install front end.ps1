# Good to know:
# Select all properties of an object: (Get-Item -Path ".\") | Select-Object -Property *
# Clone git repository: git clone https://github.com/brainy301/templates-and-scripts.git

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
Write-Output 'fontawesome installed...'

Write-Output 'installing bootstrap...'
bower install bootstrap#4.1.0
Write-Output 'bootstrap installed...'

Write-Output 'installing jquery...'
bower install jquery#3.3.1
Write-Output 'jquery installed...'

Write-Output 'downloading site icon...'
Invoke-WebRequest -Uri "http://www.iconeasy.com/icon/ico/Application/iPhonica%20Vol.%202/Notes.ico" -OutFile ("$FolderPath\images\notes.ico")

Invoke-WebRequest -Uri "http://www.iconeasy.com/icon/ico/Application/iPhonica%20Vol.%202/Notes.ico" -OutFile "D:\#\Kirk\Desktop\Notebook\Notebook.Frontend\images\notes.ico"
(New-Object System.Net.WebClient).DownloadFile("http://www.iconeasy.com/icon/ico/Application/iPhonica%20Vol.%202/Notes.ico", "D:\#\Kirk\Desktop\Notebook\Notebook.Frontend\images\notes.ico")

# Pause to check
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
#Read-Host -Prompt 'Press any key to continue'


dotnet new webapi -lang C#