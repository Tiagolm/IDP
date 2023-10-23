using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(this IApplicationBuilder app) 
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceDb = serviceScope.ServiceProvider.GetService<ApplicationContext>();

            serviceDb.Database.Migrate();
        }
    }
}
