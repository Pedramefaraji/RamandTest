using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RamandTestSolution.Application
{
    public static class SeerviceRegistration
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, System.Type type)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            services.AddMvc(option => { option.EnableEndpointRouting = false; }).AddFluentValidation();
            
            return services;
        }
    }
}
