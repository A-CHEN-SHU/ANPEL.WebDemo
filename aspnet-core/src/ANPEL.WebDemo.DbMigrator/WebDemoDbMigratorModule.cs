using ANPEL.WebDemo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ANPEL.WebDemo.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(WebDemoEntityFrameworkCoreModule),
        typeof(WebDemoApplicationContractsModule)
        )]
    public class WebDemoDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
