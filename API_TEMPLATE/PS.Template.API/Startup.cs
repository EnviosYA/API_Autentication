    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PS.Template.AccessData;
using Microsoft.EntityFrameworkCore;
using PS.Template.Domain.Commands;
using PS.Template.Domain.Service;
using PS.Template.Application.Services;
using PS.Template.AccessData.Commands;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PS.Template.AccessData.Context;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.AccessData.Repositories;
using PS.Template.Domain.Interfaces.Service;
using PS.Template.Domain.Entities;

namespace PS.Template.API
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
            services.AddMvc();

            var connectionString = Configuration.GetSection("ConnectionString").Value;
            services.AddDbContext<DbAutenticacionContext>(opcion => opcion.UseSqlServer(connectionString));

            services.AddTransient<ICuentaRepository, CuentaRepository>();
            services.AddTransient<ICuentaService, CuentaService>();

            services.AddTransient<ITipoCuentaRepository, TipoCuentaRepository>();
            services.AddTransient<ITipoCuentaService, TipoCuentaService>();

            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<IEstadoService, EstadoService>();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MicroService APIs v1.0",
                    Description = "Test services"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Habilitar swagger
            app.UseSwagger();
            //indica la ruta para generar la configuración de swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api REST");
                c.RoutePrefix = string.Empty;
            });

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
