using MiniPloomes.Repository;
using MiniPloomes.Services;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<SqlConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("MiniPloomesDb")));
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
