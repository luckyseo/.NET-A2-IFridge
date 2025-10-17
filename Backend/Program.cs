using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Services; 
using Backend.Interface;

namespace Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddCors(o => o.AddPolicy("blazor", p =>
                p.WithOrigins("http://localhost:5181").AllowAnyHeader().AllowCredentials().AllowAnyMethod()));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Repos & Services
        // builder.Services.AddScoped<IItemRepository, ItemRepository>();
        // builder.Services.AddScoped<IItemService, ItemService>();

        //register ingredient service
        builder.Services.AddScoped<IIngredientService, IngredientService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("blazor");
        //app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();
        //app.MapGet("/api/test", () => new { message = "Hi from API" });

        app.Run();
    }
}