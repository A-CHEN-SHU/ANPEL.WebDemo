using System.Threading.Tasks;

namespace ANPEL.WebDemo.Data
{
    public interface IWebDemoDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
