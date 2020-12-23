using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using System;
using System.Text;
using TimeManagement.Api.Context.Authentication;
using TimeManagement.Api.Context.TimeManagement;
using TimeManagement.Api.Services.Entries;
using TimeManagement.Api.Services.Users;
using TimeManagement.Api.Settings;
using TimeManagement.BL.Services;
using TimeManagementApi.Context.Authentication;
using TimeManagementApi.Settings;

namespace TimeManagementApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSettings jwtSettings = Configuration.GetSection("JWT").Get<JwtSettings>();

            string userConnectionString = Configuration.GetConnectionString("TimeManageUser");

            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(
                o => o.UseMySql(userConnectionString,
                new MySqlServerVersion(new Version(8, 0, 21)),
                mySqlOptions =>
                   {
                       mySqlOptions.CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                   }).EnableDetailedErrors());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            string dataConnectionString = Configuration.GetConnectionString("TimeManageData");
            services.AddDbContext<TimeManagementDbContext>(
                o => o.UseMySql(dataConnectionString,
                new MySqlServerVersion(new Version(8, 0, 21)),
                mySqlOptions =>
                {
                    mySqlOptions.CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                }).EnableDetailedErrors());

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
                };
            });

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeManagementApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT token into field"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new string[]{}
                    }
                });
            });

            services.AddCors(o =>
            {
                o.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddTransient<ICurrentUserProvider, CurrentUserFromHttpContext>();
            services.AddTransient<IUserRepository, UserRepositoryDb>();
            services.AddTransient<ITimeEntryRepository, TimeEntryRepositoryDb>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeManagementApi v1"));
            }
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AdminSettings admin = Configuration.GetSection("Admin").Get<AdminSettings>();
            ApplicationDbInitializer.SeedAdminUser(userManager, admin);
        }
    }
}
