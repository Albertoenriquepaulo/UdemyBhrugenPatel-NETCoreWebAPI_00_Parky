# RESTful API with ASP.NET Core Web API - Create and Consume. 
Udemy Course Training by BhrugenPatel

## Important Notes
### DTOs Creating AutoMappers

1. Add extensions below

```
 AutoMapper
 AutoMapper.Extensions.Microsoft.DependencyInjection
```
2. Watch video **16. Auto Mapper Configuration**

### Add Swashbuckle

Instalar Nuget Package **Swashbuckle.AspNetCore**

After installed the new nuget package it's tiem to configure the middleware, Watch video 26

Add this to startup -> ConfigureServices

```c#
services.AddSwaggerGen(options =>
	{
	options.SwaggerDoc("ParkyOpenApiSpec", new Microsoft.OpenApi.Models.OpenApiInfo()
    	{
        Title = "Parky API",
        Version = "1",
		});
```

Add this to startup -> Configure

```c#
app.UseSwagger();	
```

Now we can open this URL in the browser

```shell
https://localhost:44333/swagger/ParkyOpenApiSpec/swagger.json
```

Watch commit _26. Adding Swashbuckle to API_

In this [link](https://docs.microsoft.com/es-es/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio), there are more information about **_Swashbuckle y ASP.NET Core_**

 To show the UI we need to add this to the startup -> Configure

``` C#
app.UseSwagger();
app.UseSwaggerUI(options =>
	{
     options.SwaggerEndpoint("/swagger/ParkyOpenApiSpec/swagger.json", "Parky API");
    });
```

#### Setting swagger page as home page

Add this to startup -> Configure

```c#
app.UseSwaggerUI(options =>
{
options.SwaggerEndpoint("/swagger/ParkyOpenApiSpec/swagger.json", "Parky API");
options.RoutePrefix = "";
});
```

And delete the _"launchUrl": "weatherforecast",_ from properties -> launchSettings.json, see Video 28

#### Setting the swagger page with comments in the code of each function

See Video 29

#### Ignoring XML Comments Warnings

See Video 30

#### Api Versioning

```shell
Install NugetPcket -> Microsoft.AspNetCore.Mvc.Versioning		
```

See Video 41





