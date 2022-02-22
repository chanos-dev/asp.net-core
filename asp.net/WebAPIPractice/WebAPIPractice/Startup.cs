using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPractice
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

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TEST Swagger Service API",
                    Version = "v1",
                    Description = "API 문서 설명하는 부분",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "chanos.dev@gmail.com",
                        Name = "chanos.dev",
                        Url = new Uri("https://github.com/chanos-dev"),
                    },

                });

                swagger.SwaggerDoc("other", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "other Swagger Service API",
                    Version = "v1",
                    Description = "API 문서 설명하는 부분2",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "chanos.dev@gmail.com",
                        Name = "chanos.dev",
                        Url = new Uri("https://github.com/chanos-dev"),
                    },

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
