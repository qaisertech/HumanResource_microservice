using Employee.Microservice.Data;
using Employee.Microservice.IRepository;
using Employee.Microservice.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Microservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeConnectionString")));
            //services.AddAuthentication("Bearer")
            //   .AddJwtBearer("Bearer", opt =>
            //   {
            //       opt.RequireHttpsMetadata = false;
            //       opt.Authority = "https://localhost:5005";
            //       opt.Audience = "employeeApi";
            //   });

           services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.Authority = "https://localhost:5005";
                    opt.Audience = "employeeApi";
                });
            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultScheme = "Cookies";
            //    opt.DefaultChallengeScheme = "oidc";
            //}).AddCookie("Cookies")
            //.AddOpenIdConnect("oidc", opt =>
            //{
            //    opt.SignInScheme = "Cookies";
            //    opt.Authority = "https://localhost:5005";
            //    opt.ClientId = "employee-client";
            //    opt.ResponseType = "code id_token";
            //    opt.SaveTokens = true;
            //    opt.ClientSecret = "EmployeeSecret";
            //    opt.GetClaimsFromUserInfoEndpoint = true;

            //    opt.ClaimActions.DeleteClaim("sid");
            //    opt.ClaimActions.DeleteClaim("idp");

            //    opt.Scope.Add("address");
            //    opt.Scope.Add("roles");
            //    opt.ClaimActions.MapUniqueJsonKey("role", "role");
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        RoleClaimType = "role"
            //    };
            //});
            services.AddCors();
            services.AddControllersWithViews();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseCors(x =>
            {
                x.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
