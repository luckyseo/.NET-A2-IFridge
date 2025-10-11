using Backend.AppData;
using Backend.Services;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

            //register db here
            builder.Services.AddDbContext<AppDbContext>(FileOptions => FileOptions.UseSqlite("Data Source= smart fridge.db"));


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddCors(o => o.AddPolicy("blazor", p =>
                p.WithOrigins("https://localhost:5173").AllowAnyHeader().AllowCredentials().AllowAnyMethod()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Repos & Services
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("blazor");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapGet("/api/test", () => new { message = "Hi from API" });

            app.Run();
        }
    }
}
