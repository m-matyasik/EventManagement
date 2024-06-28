using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("EventMgmtDb"));

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IVenueRepository, VenueRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}