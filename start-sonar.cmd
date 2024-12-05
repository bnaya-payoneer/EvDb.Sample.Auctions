@echo off
REM Install .NET global tool for SonarCloud scanner
dotnet tool install -g dotnet-sonarscanner

REM Begin SonarCloud analysis
REM Replace placeholders with your actual values
dotnet sonarscanner begin ^
  /k:"your-project-key" ^
  /o:"your-organization" ^
  /d:sonar.login="your-sonar-token" ^
  /d:sonar.host.url="https://sonarcloud.io" ^
  /d:sonar.cs.roslyn.reportPaths="*.sarif" ^
  /d:sonar.sourceEncoding="UTF-8"

dotnet build EvDb.Sample.Auctions.sln

# End SonarCloud analysis
dotnet sonarscanner end /d:sonar.login="a6f337914f2856a304a2c60922caed8a2682b51c"