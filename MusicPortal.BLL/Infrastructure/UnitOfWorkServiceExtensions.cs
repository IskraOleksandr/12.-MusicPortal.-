using Microsoft.Extensions.DependencyInjection;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
