﻿using Autoglass.Aplicacao.Interfaces;
using Autoglass.Aplicacao.Mapping;
using Autoglass.Aplicacao.Services;
using Autoglass.Infra.Context;
using Autoglass.Infra.Repository;
using Microsoft.OpenApi.Models;

namespace AutoglassAPI.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddScoped<IAutoglassService, AutoglassService>();
            services.AddScoped<IAutoglassRepository, AutoglassRepository>();
            services.AddScoped<IAutoglassContext>(provider => provider.GetService<AutoglassContext>());

            //configuracao de dateonly do swagger
            services.AddSwaggerGen( c =>
                c.MapType<DateOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date"
                }));

            return services;
        }
    }
}
