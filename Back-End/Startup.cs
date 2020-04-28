using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Repositories;
using Back_End.Services;
using JiaDungDao.Connection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace JiaDungDao {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext> (options => options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddControllers ();

            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy",
                    builder => {
                        builder.WithOrigins("http://localhost:8080")
                            .AllowAnyHeader ()
                            .AllowAnyMethod ()
                            .AllowCredentials();
                    });
            });

            var tokenParams = new TokenValidationParameters () {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = Configuration["JWT:issuer"],
                ValidAudience = Configuration["JWT:audience"],
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8
                .GetBytes (Configuration["JWT:key"]))
            };
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (jwtconfig => {
                    jwtconfig.TokenValidationParameters = tokenParams;
                });

            #region Restaurant
            services.AddScoped<IRestaurantService, RestaurantService> ();
            services.AddScoped<IRestaurantRepo, RestaurantRepo> ();
            #endregion
            #region Member
            services.AddScoped<IMemberService, MemberService> ();
            services.AddScoped<IMemberRepo, MemberRepo> ();
            #endregion
            #region Order
            services.AddScoped<IOrderService, OrderService> ();
            services.AddScoped<IOrderRepo, OrderRepo> ();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseCors ("CorsPolicy");
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }else
            {
                app.UseHsts();
            }

            

            app.UseHttpsRedirection ();

            app.UseRouting ();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async(context) => {
                await context.Response.WriteAsync("Could Not Find Anything");
            });
        }
    }
}