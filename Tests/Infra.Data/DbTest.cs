using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Infra.Data
{
    public class DbTest : IDisposable
    {
        private string _databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider ServiceProvider { get; set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationContext>(o =>
                o.UseSqlServer($"Persist Security Info=true;Server=DESKTOP-NN5HHMM;Database={_databaseName};Trusted_Connection=True;MultipleActiveResultSets=true;"),
                ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<ApplicationContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<ApplicationContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
