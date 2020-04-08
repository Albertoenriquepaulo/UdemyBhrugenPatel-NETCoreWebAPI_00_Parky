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



