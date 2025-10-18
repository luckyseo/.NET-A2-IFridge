using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Frontend.Service;
using Frontend.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
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
            builder.Services.AddScoped<LoginService>(
                sp => {
                    var http = new HttpClient
                    {
                        BaseAddress = new Uri("http://localhost:5037") // Backend URL
                    };
                var sessionStorage = sp.GetRequiredService<ProtectedSessionStorage>();
                return new LoginService(http,sessionStorage);
                }
            );
            builder.Services.AddScoped<ShoppingListService>();
            builder.Services.AddScoped<IngredientService>();
            builder.Services.AddScoped<ItemService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<Frontend.Components.App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
//connect to backend  program.cs
