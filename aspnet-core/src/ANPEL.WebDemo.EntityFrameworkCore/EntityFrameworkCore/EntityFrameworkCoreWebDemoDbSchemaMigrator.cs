using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ANPEL.WebDemo.Data;
using Volo.Abp.DependencyInjection;

namespace ANPEL.WebDemo.EntityFrameworkCore
{
    public class EntityFrameworkCoreWebDemoDbSchemaMigrator
        : IWebDemoDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreWebDemoDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the WebDemoDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<WebDemoDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
