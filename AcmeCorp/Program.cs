using AcmeCorp.Externsions;
using AcmeCorp.Models;
using AcmeCorp.Repositories;
using AcmeCorp.Services;
using AcmeCorps.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AcmeCorp API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", //Name the security scheme
   new OpenApiSecurityScheme
   {
       Description = "JWT Authorization header using the Bearer scheme.",
       Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
       Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
   });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Id = "Bearer", //The name of the previously defined security scheme.
                Type = ReferenceType.SecurityScheme
            }
        },new List<string>()
    }});
});

builder.Services.AddDbContext<AcmeCorpContext>();

var appSettingsSection = builder.Configuration.GetSection("ServiceConfiguration");
builder.Services.Configure<ServiceConfiguration>(appSettingsSection);
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderValidator, OrderValidator>();

// configure jwt authentication
var serviceConfiguration = appSettingsSection.Get<ServiceConfiguration>();
var JwtSecretkey = Encoding.ASCII.GetBytes(serviceConfiguration.JwtSettings.Secret);
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
    ValidateIssuer = false,
    ValidateAudience = false,
    RequireExpirationTime = false,
    ValidateLifetime = true
};
builder.Services.AddSingleton(tokenValidationParameters);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
 );
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["ServiceConfiguration:JwtSettings:Secret"]);
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, //TODO: TW on production make it true
        ValidateAudience = false, // on production make it true
        ValidateLifetime = false,
        ValidIssuer = "http://localhost:32778",
        ValidAudience = "https://localhost:32778",
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ValidateIssuerSigningKey = true,
        ClockSkew = TokenValidationParameters.DefaultClockSkew
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});


var app = builder.Build()
    .MigrateDatabase<AcmeCorpContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    string token = context.Request.Headers[app.Configuration["ServiceConfiguration:JwtSettings:TokenName"]];

    if (!string.IsNullOrEmpty(token))
    {
        var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userIdentity = new ClaimsIdentity(jwttoken.Claims);
        context.User = new ClaimsPrincipal(userIdentity);
    }

    await next();

});
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
