::dotnet restore src\UiMatic\project.json
::dotnet pack src\UiMatic\project.json -c Release -o artifacts\bin\UiMatic\Release

::set /p version="Version: "
::nuget push artifacts\bin\UiMatic\Release\UiMatic.%version%.nupkg

dotnet restore src\UiMatic.SeleniumWebDriver\project.json
dotnet pack src\UiMatic.SeleniumWebDriver\project.json -c Release -o artifacts\bin\UiMatic.SeleniumWebDriver\Release

set /p version="Version: "
nuget push artifacts\bin\UiMatic.SeleniumWebDriver\Release\UiMatic.SeleniumWebDriver.%version%.nupkg