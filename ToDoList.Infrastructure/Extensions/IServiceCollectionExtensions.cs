using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Common;
using ToDoList.Domain.Common.Interfaces;
using ToDoList.Infrastructure.Services;

namespace ToDoList.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>();
        }
    }
}
