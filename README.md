# ASP.NET 3.1 project
## Technologies
- ASP.Net Core 3.1
- Entity Framework Core 3.1
## Install Entity Frameword Core 3.1
Link: https://docs.microsoft.com/en-us/ef/core/get-started/install/
Install Package: 
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
##Add Migration
Install Package:
- Microsoft.Extensions.Configuration.FileExtensions (ConfigurationBuilder().SetBasePath)
- Microsoft.Extensions.Configuration.Json (ConfigurationBuilder().AddJsonFile)
Run Package Manager Console
- "Add-migration intail" (project eShopSolution.Data)
- To undo this action, use "Remove-Migration"
- "Update-database"
## Youtube