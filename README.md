https://en.wikipedia.org/wiki/Markdown#Example
# ASP.NET 3.1 project
## Technologies
1. ASP.Net Core 3.1
2. Entity Framework Core 3.1
## Install Entity Frameword Core 3.1
Link: https://docs.microsoft.com/en-us/ef/core/get-started/install/
Install Package: 
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Tools
## Add Migration
Install Package:
* Microsoft.Extensions.Configuration.FileExtensions (ConfigurationBuilder().SetBasePath)
* Microsoft.Extensions.Configuration.Json (ConfigurationBuilder().AddJsonFile)
Run Package Manager Console
~~~bash
Add-migration intail
~~~
To undo this action, use
~~~bash
Remove-Migration
~~~
Final, run
~~~bash
Update-database
~~~
## Data Seeding