using ImaginaryRealEstate;
using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Authorization;
using ImaginaryRealEstate.Middlewares;
using ImaginaryRealEstate.Services;
using ImaginaryRealEstate.Validators;
using MongoFramework;

var builder = WebApplication.CreateBuilder(args);

// Logger configuration
builder.Logging.AddMyLogger();

// Database Context
builder.Services.AddSingleton<DomainDbContext>();
// Authorization
builder.Services.AddAuthorizationCustom();

// Model Validators from FluentValidator
builder.Services.AddValiadators();

// Services
builder.Services.AddServices();

// Middlewares
builder.Services.AddMiddlewares();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthenticationCustom(builder);

var app = builder.Build();

app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        // .WithOrigins("https://localhost:4200")); // Allow only this origin can also have multiple origins separated with comma
        .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();