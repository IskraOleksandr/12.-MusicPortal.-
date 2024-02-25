using Microsoft.Extensions.DependencyInjection;
using MusicPortal.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.BLL.Infrastructure
{
     public static class MusicPortalContextExtensions
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<Music_PortalContext>(options => options.UseSqlServer(connection));
        }
    }
}
