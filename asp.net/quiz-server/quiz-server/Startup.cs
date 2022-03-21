using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using quiz_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_server
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
            services.AddControllers();


            // mysql entity framework
            services.AddDbContext<QuizDbContext>(options =>
            {
                options.UseMySQL(connectionString: Configuration.GetConnectionString(name: "DevConnection"));
            });


            // swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Quiz Service API",
                    Version = "v1",
                    Description = @"CloneCoding - https://www.youtube.com/watch?v=MV1rEWlcW7U",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "chanos.dev@gmail.com",
                        Name = "chanos.dev",
                        Url = new Uri("https://github.com/chanos-dev"),
                    },
                }); 
            });

            // cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();

                    policy.WithOrigins("http://localhost:3000"); 
                    policy.WithOrigins("http://192.168.0.101:3000"); 
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                swagger.SwaggerEndpoint("/swagger/other/swagger.json", "other");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();
            //app.UseCors(options =>
            //{
            //    options.AllowAnyMethod();
            //    options.AllowAnyHeader();

            //    options.WithOrigins("http://localhost:3000");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
