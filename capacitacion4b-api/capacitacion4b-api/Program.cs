using capacitacion4b_api.Data;
using capacitacion4b_api.Data.interfaces;
using capacitacion4b_api.Data.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

postgresqlConection postgresqlConection = new (Environment.GetEnvironmentVariable ("CONNECTION_STRING"));
builder.Services.AddSingleton(postgresqlConection);

builder.Services.AddScoped<iUserService, userService>();
builder.Services.AddScoped<iTaskService, taskService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
