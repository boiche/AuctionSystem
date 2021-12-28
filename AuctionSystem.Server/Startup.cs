using AuctionSystem.Data.Model;
using AuctionSystem.Server.Chat;
using AuctionSystem.Server.Services;
using AuctionSystem.Server.Services.Interfaces;
using AuctionSystem.Server.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AuctionSystem.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });           
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    //In order that 44305 and 4735 don't work, use 8080 with running vue
                    builder.WithOrigins("http://localhost:8080", "http://localhost:8081", "https://localhost:44305", "http://localhost:4735")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddControllers();
            services.AddDbContext<AuctionSystemContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuctionSystemConnectionString")));
            services.AddScoped<IUserService, UserService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();
                app.UseDefaultFiles(); // Enables default file mapping on the web root.
                app.UseStaticFiles(); // Marks files on the web root as servable.               
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseMiddleware<JWTMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
                endpoints.MapHub<ChatHub>("/chat");
            });
            
        }
    }
}
