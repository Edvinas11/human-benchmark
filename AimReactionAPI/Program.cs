using AimReactionAPI.Data;
using AimReactionAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var frontendUrl = builder.Configuration["Frontend:Url"];
if (string.IsNullOrWhiteSpace(frontendUrl))
{
    throw new ArgumentNullException("Frontend:Url", "Frontend URL is not configured. Please set 'Frontend:Url' in appsettings.json or environment variables.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
    b => {
        b.WithOrigins(frontendUrl)
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<TargetService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton(typeof(GameSessionHandler<>));

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
