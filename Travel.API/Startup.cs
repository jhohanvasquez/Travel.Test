using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Travel.Business.Configuration;
using Travel.Business.Contracts;
using Travel.Business.Entities;
using Travel.Business.Interfaces;
using Travel.Business.Services;
using Travel.DataAccess.Common;
using Travel.DataAccess.Config;
using Travel.DataAccess.DTO;
using Travel.DataAccess.Projection;
using Travel.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace Travel.API
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<DataBaseSettings>(Configuration.GetSection(typeof(DataBaseSettings).Name));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            IMapper mapper = new MapperConfiguration(map =>
            {
                map.AddProfile(new AutomapperProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);

            services.Configure<ApiSettings>(Configuration);

            services.AddTransient<ITableRepository<DataLogDTO>>(s => new TableRepository<DataLogDTO>(Configuration.GetValue<string>("environmentVariables:StorageSettings:ConnectionString"), Configuration.GetValue<string>("environmentVariables:StorageSettings:SafeDataTable")));
            services.AddTransient<IRestClient<CommandResponse>>(s => new RestClient<CommandResponse>(Configuration.GetValue<string>("environmentVariables:RestSettings:TravelApi")));
            services.AddTransient<IRestClient<List<int>>>(s => new RestClient<List<int>>(Configuration.GetValue<string>("environmentVariables:RestSettings:TravelApi")));

            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IFlightBDRepository, FlightBDRepository>();
            services.AddScoped<ILogRegister, LogRegister>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Travel", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Travel.API.xml");
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                           {   new OpenApiSecurityScheme {
                                   Reference = new OpenApiReference {
                                       Type = ReferenceType.SecurityScheme,
                                       Id = "Bearer"
                                   },
                                   Scheme = "token",
                                   Name = "Bearer",
                                   In = ParameterLocation.Header
                               },
                               new List<string>()
                           }
                       });

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                           {   new OpenApiSecurityScheme {
                                   Reference = new OpenApiReference {
                                       Type = ReferenceType.SecurityScheme,
                                       Id = "bearer"
                                   },
                                   Scheme = "token",
                                   Name = "bearer",
                                   In = ParameterLocation.Header
                               },
                               new List<string>()
                           }
                       });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Safe api V1");
                c.RoutePrefix = "swagger";
            });

            app.UseMvc();
        }
    }
}
