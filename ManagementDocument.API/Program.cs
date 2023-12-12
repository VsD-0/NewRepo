using FluentValidation;
using FluentValidation.AspNetCore;

using ManagementDocument.API.Behaviors;
using ManagementDocument.API.Exceptions;
using ManagementDocument.API.Extensions;
using ManagementDocument.API.Filters;
using ManagementDocument.API.MiddleWares;
using ManagementDocument.API.Models;
using ManagementDocument.API.Services;
using ManagementDocument.API.Validations;
using ManagementDocument.Database.Context;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day,
                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
.CreateLogger();

builder.Services.AddControllers(o =>
{
}).AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
                opt.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API",
        Description = "ManagementDocument API",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Введите токен авторизации.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {

                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            new List<string>()
        }
    });

    c.EnableAnnotations();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    c.SchemaFilter<EnumTypesSchemaFilter>(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.DocumentFilter<EnumTypesDocumentFilter>();
    c.OperationFilter<EnumTypesOperationFilter>();

    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

builder.Services.AddProblemDetails(options =>
{
    options.Map<CustomException>(ex =>
    {
        var problemDetails = new CustomProblemDetails
        {
            Title = "Произошла ошибка",
            Detail = ex.Message,
            Errors = ex.Errors
        };
        return problemDetails;
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.ConfigureJwtAuthentication(jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings), "Null"));

builder.Services.AddDbContext<ManagementDocumentDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IDocumentService, DocumentService>();
builder.Services.AddTransient<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDocumentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateDocumentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeleteDocumentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AuthValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegistrationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetSortDocumentsValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI((c) =>
    {
        c.DocumentTitle = "ManagementDocument - Swagger docs";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManagementDocument API v1");
        c.EnableDeepLinking();
        c.DefaultModelsExpandDepth(0);
    });
}

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
