using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers( );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( options =>
  {
      options.EnableAnnotations( );

      options.AddSecurityDefinition( "Bearer", new OpenApiSecurityScheme
      {
          Description = "Entre com o Token JWT",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
      } );

      options.AddSecurityRequirement( new OpenApiSecurityRequirement
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Id = "Bearer",
                      Type = ReferenceType.SecurityScheme
                  }
              }, new List<string>()
          }
      } );
  }
);

builder.Services.ConfigureDependenciesService( );
builder.Services.ConfigureDependenciesRepository( );

var config = new AutoMapper.MapperConfiguration( cfg =>
{
    cfg.AddProfile( new DtoToModelProfile( ) );
    cfg.AddProfile( new EntityToDtoProfile( ) );
    cfg.AddProfile( new ModelToEntityProfile( ) );
} );

IMapper mapper = config.CreateMapper( );
builder.Services.AddSingleton( mapper );

var signingConfigurations = new SigningConfigurations( );
builder.Services.AddSingleton( signingConfigurations );

var tokenConfigurations = new TokenConfigurations( );

new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration
        .GetSection( "TokenConfigurations" ) )
        .Configure( tokenConfigurations );

builder.Services.AddSingleton( tokenConfigurations );

builder.Services.AddAuthentication( authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
} ).AddJwtBearer( bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

    // Valida a assinatura de um token recebido
    paramsValidation.ValidateIssuerSigningKey = true;

    // Verifica se um token recebido ainda é válido
    paramsValidation.ValidateLifetime = true;

    // Tempo de tolerância para a expiração de um token (utilizado
    // caso haja problemas de sincronismo de horário entre diferentes
    // computadores envolvidos no processo de comunicação)
    paramsValidation.ClockSkew = TimeSpan.Zero;
} );

builder.Services.AddAuthorizationBuilder( )
    .AddPolicy( "Bearer", new AuthorizationPolicyBuilder( )
        .AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme‌​ )
        .RequireAuthenticatedUser( ).Build( ) );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment( ) )
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseHttpsRedirection( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );