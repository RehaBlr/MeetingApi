using MeetingApi.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MeetingApi.Infrastructure.Context;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MeetingDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DemoDb")));
//add authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("SecretKey"))),
        };
    });

//currency service resolver

#region Scrutor resolvers

var typeBaseService = typeof(BaseService);

var assembly = typeBaseService.Assembly;

builder.Services.Scan(selector =>
        selector
            .FromAssemblies(assembly)
            .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );



var singletonBaseAssembly = typeof(BaseSingletonService).Assembly;
builder.Services.Scan(selector =>
        selector
            .FromAssemblies(singletonBaseAssembly)
            .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseSingletonService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );
#endregion

var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseCustomException();

app.Run();
