using ANPEL.WebDemo.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace ANPEL.WebDemo.Product
{
    /// <summary>
    /// 设置实现
    /// </summary>
    public class WebDemoSettingsAppService : WebDemoAppService, IWebDemoSettingsAppService
    {
        public ISettingManager _settingManager { set; get; }

        public async Task<WebDemoSettingsDto> GetAsync()
        {
            // 1、创建设置对象
            WebDemoSettingsDto webDemoSettingsDto = new WebDemoSettingsDto();
            webDemoSettingsDto.nativeUrl = await _settingManager.GetOrNullGlobalAsync(WebDemoSettings.WxPay.NativeUrl);
            webDemoSettingsDto.mchid = await _settingManager.GetOrNullGlobalAsync(WebDemoSettings.WxPay.Mchid);
            webDemoSettingsDto.certpath = await _settingManager.GetOrNullGlobalAsync(WebDemoSettings.WxPay.Certpath);
            webDemoSettingsDto.certSerialNo = await _settingManager.GetOrNullGlobalAsync(WebDemoSettings.WxPay.CertSerialNo);

            return webDemoSettingsDto;
        }

        public async Task UpdateAsync(WebDemoSettingsUpdateDto input)
        {
            await _settingManager.SetGlobalAsync(WebDemoSettings.WxPay.NativeUrl, input.nativeUrl);
            await _settingManager.SetGlobalAsync(WebDemoSettings.WxPay.Mchid, input.mchid);
            await _settingManager.SetGlobalAsync(WebDemoSettings.WxPay.Certpath, input.certpath);
            await _settingManager.SetGlobalAsync(WebDemoSettings.WxPay.CertSerialNo, input.certSerialNo);
        }
    }
}
