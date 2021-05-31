using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<Application.Interfaces.IUserRepository , Persistence.Repositories.UserRepository> ();
            services.AddTransient<Application.Interfaces.IUnitOfWorks    , Persistence.Repositories.UnitOfWork> ();

        }
    }
}
