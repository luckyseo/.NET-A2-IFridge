using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Frontend.Service;
using Frontend.Models;
//resgister service here 
// 

namespace Frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
               .AddInteractiveServerComponents();


            //Testing the front end 

            // Add services to the container.
            // builder.Service.AddRazorPages();
            // builder.Service.AddServerSideBlazor();

            builder.Services.AddHttpClient();
            // Register your services here:
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddSingleton<ShoppingListService>();
            builder.Services.AddSingleton<IngredientService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<Frontend.Components.App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
//connect to backend  program.cs
