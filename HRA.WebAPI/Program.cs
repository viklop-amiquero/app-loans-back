using HRA.Application;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Pagination;
using HRA.Infrastructure.Persistence;
using HRA.Transversal;
using HRA.WebAPI;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Personalizado: Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddTransversalServices(builder.Configuration, builder.Host);
builder.Services.AddApiServices();
builder.Services.AddScoped<IPagedList, PagedList>();

// Add services to the container.
builder.Services.AddControllers();

#region JWT_Authentication


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {new OpenApiSecurityScheme
            {
             Reference = new OpenApiReference
             {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
             }
          },
          new string[] {}
         }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


#endregion

var app = builder.Build();

#region IsDevelopment

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.Ligin v1 - Dev"));
}
else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.BackEnd v1 - QA"));
}

#endregion

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
