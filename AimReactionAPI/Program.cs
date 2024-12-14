using AimReactionAPI.Data;
using AimReactionAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
    b => {
        b.WithOrigins(
            builder.Configuration["Frontend:Url"] ?? throw new ArgumentNullException("Frontend:Url"),
            "https://octopus-app-43esm.ondigitalocean.app"
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<TargetService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton(typeof(GameSessionHandler<>));

var app = builder.Build();

// app.UseCors("AllowFrontend");
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/favicon.ico"))
    {
        context.Response.StatusCode = 204;
        return;
    }
    await next();
});

app.Run();
