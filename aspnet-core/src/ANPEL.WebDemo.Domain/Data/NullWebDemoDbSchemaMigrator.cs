using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ANPEL.WebDemo.Data
{
    /* This is used if database provider does't define
     * IWebDemoDbSchemaMigrator implementation.
     */
    public class NullWebDemoDbSchemaMigrator : IWebDemoDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}