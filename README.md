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
Create Token by jWT
```c#
public async Task<string> Authencate(LoginRequest request)
{
    var user = await _userManager.FindByNameAsync(request.UserName);
    if (user == null) return null;

    var result = await _signinManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
    if (!result.Succeeded) return null;

    var roles = _userManager.GetRolesAsync(user);
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, user.LastName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.GivenName, user.FirstName),
        new Claim(ClaimTypes.Role, string.Join(";",roles))
    };
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
        _config["Tokens:Issuer"],
        claims,
        expires: DateTime.Now.AddHours(3),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```
Setup in Startup.cs function ConfigureServices
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
And setup function Configure
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
## Call Api
```C#
public async Task<string> Authenticate(LoginRequest request)
{
    var json = JsonConvert.SerializeObject(request);
    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

    var client = _httpClientFactory.CreateClient();
    client.BaseAddress = new Uri("https://localhost:5001");
    var response = await client.PostAsync("/api/users/authenticate", httpContent);
    var token = await response.Content.ReadAsStringAsync();
    return token;
}
```
## Cookie Authentication without ASP.NET Identity
Link https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-3.1
Write a funtion ValidateToken
```C#
private ClaimsPrincipal ValidateToken(string jwtToken)
{
    IdentityModelEventSource.ShowPII = true;

    SecurityToken validatedToken;
    TokenValidationParameters validationParameters = new TokenValidationParameters();

    validationParameters.ValidateLifetime = true;

    validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
    validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
    validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

    ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

    return principal;
}
```

```C#
[HttpPost]
public async Task<IActionResult> Login(LoginRequest request)
{
    if (!ModelState.IsValid)
        return View(ModelState);

    var token = await _userApiClient.Authenticate(request);

    var userPrincipal = this.ValidateToken(token);
    var authProperties = new AuthenticationProperties
    {
        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        IsPersistent = false
    };
    await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties);

    return RedirectToAction("Index", "Home");
}
```