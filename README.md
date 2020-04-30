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
* Microsoft.Extensions.Configuration
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
## Manage Image
## Add Swagger
~~~bash
Install-Package Swashbuckle.AspNetCore -Version 5.0.0
~~~
## Add JWT
```c#
public void ConfigureServices(IServiceCollection services)
{
	// some code here
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                Enter 'Bearer' [space] and then your token in the text input below.
                \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
                {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
            }
        });
    });

    // some code here

    string issuer = Configuration.GetValue<string>("Tokens:Issuer");
    string signingKey = Configuration.GetValue<string>("Tokens:Key");
    byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

    services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = System.TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
    });
	// some code here
}
```
And
```C#
 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	// some code here
    app.UseAuthentication();
	// some code here
}
```
## Fluent Validation
https://docs.fluentvalidation.net/en/latest/aspnet.html
~~~bash
Install-Package FluentValidation.AspNetCore
~~~
## Admin App
Templates: https://startbootstrap.com/templates/
