using ImaginaryRealEstate;
using ImaginaryRealEstate.Authentication;
using ImaginaryRealEstate.Authorization;
using ImaginaryRealEstate.Middlewares;
using ImaginaryRealEstate.Services;
using ImaginaryRealEstate.Validators;

var builder = WebApplication.CreateBuilder(args);

// This is needed to Datetime works properly
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Logger configuration
builder.Logging.AddMyLogger();

// Database Context
builder.Services.AddDbContext<DomainDbContext>();

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