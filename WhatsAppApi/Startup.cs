using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WhatsAppApi.Data;
using WhatsAppApi.Hubs;
using WhatsAppApi.Helpers;
using WhatsAppApi.Repositories;

namespace WhatsAppApi
{
    public class Startup
    {
        private string[] ALLOWED_ORIGINS = new string[] { "http://localhost:3000", "https://localhost:44388" };
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private string secretKey = "nQjRUnJ4230n6iWLbfoGJMqToMd3GUsanQjRUnJ4230n6iWLbfoGJMqToMd3GUsapiMP2KVr28SPd9pAvoeGzdHOAIrhaVh";


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidAudience = "",
                    //ValidIssuer ="",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddCors();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
        Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WhatsAppApi", Version = "v1" });
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WhatsAppApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
             .WithOrigins(ALLOWED_ORIGINS)
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials()
             );

            // New Code 


            //app.UseJwtBearerAuthentication(new JwtBearerOptions()
            //{
            //    Audience = "http://localhost:5001/",
            //    Authority = "http://localhost:5000/",
            //    AutomaticAuthenticate = true
            //});

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
