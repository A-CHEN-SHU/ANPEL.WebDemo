using Volo.Abp.Settings;

namespace ANPEL.WebDemo.Settings
{
    public class WebDemoSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(WebDemoSettings.MySetting1));
            // 1、定义微信支付设置
            context.Add(
                new SettingDefinition(WebDemoSettings.WxPay.NativeUrl),
                new SettingDefinition(WebDemoSettings.WxPay.Mchid),
                new SettingDefinition(WebDemoSettings.WxPay.Certpath),
                new SettingDefinition(WebDemoSettings.WxPay.CertSerialNo));
        }
    }
}
