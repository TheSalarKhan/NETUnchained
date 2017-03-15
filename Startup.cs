using Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;

namespace Application
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public static string CONNECTION_STRING { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // This reads the connection string from appsettings.json
            CONNECTION_STRING = Configuration.GetConnectionString("DatabaseConnection");
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add EF Core.
            services
            .AddEntityFrameworkSqlServer()
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(CONNECTION_STRING));

            // Any controller that extends from the base controller will get injected with
            // the database context.
            services.AddScoped<Application.Controllers.BaseController>();
            
            // Add framework services.
            services.AddMvc();

            /*Adding swagger generation with default settings*/
            services.AddSwaggerGen(options => {
                options.SingleApiVersion(new Info{
                    Version="v1",
                    Title="Auth0 Swagger Sample API",
                    Description="API Sample made for Auth0",
                    TermsOfService = "None"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            
            app.SeedData();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            /*Enabling swagger file*/
            app.UseSwagger();
            /*Enabling Swagger ui, consider doing it on Development env only*/
            app.UseSwaggerUi();
            
            app.UseMvc();
        }
    }
}