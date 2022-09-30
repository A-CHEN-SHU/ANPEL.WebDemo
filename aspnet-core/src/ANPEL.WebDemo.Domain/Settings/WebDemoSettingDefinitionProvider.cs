using Volo.Abp.Settings;

namespace ANPEL.WebDemo.Settings
{
    public class WebDemoSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(WebDemoSettings.MySetting1));
        }
    }
}
