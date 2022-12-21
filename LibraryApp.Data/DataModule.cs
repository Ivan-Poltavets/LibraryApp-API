using LibraryApp.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data
{
    public static class DataModule
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}
