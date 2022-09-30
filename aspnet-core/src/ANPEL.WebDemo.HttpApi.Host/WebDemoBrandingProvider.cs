using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ANPEL.WebDemo
{
    [Dependency(ReplaceServices = true)]
    public class WebDemoBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "WebDemo";
    }
}
