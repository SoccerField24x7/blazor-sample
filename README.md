# Hidden Villa Sample Blazor Project

This code is the result of Udemy course [Blazor - The Complete Guide (WASM & Server, .NET Core)](https://www.udemy.com/course/blazor-the-complete-guide/)

## Running the Project

### SQL SERVER

You will need an instance of MS SQL Server in order to run this project. Docker is the obvious choice:

`docker run --name sqlserver-local -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Allowmein44$$' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest`

If you use the above, the project will connect without any further changes.  Otherwise, you'll need to update the `appsettings.json` file located in the HiddenVilla_Server project directory.
