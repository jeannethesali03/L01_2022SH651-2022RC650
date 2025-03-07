using L01_2022SH651_2022RC650.Models;
using Microsoft.EntityFrameworkCore;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyeccion por dependencia de String de conexion al contexto
builder.Services.AddDbContext<BlogContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("blogDbConnection")
            )
 );



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
