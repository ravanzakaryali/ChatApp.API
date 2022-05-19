using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Business.Profiles
{
    public static class MapperServiceExtensions
    {
       public static void AddMapperService(this IServiceCollection services)
        {
            services.AddScoped(provider => new MapperConfiguration(mp =>
            {
                mp.AddProfile(new Mapper());
            }).CreateMapper());
        }
    }
}
