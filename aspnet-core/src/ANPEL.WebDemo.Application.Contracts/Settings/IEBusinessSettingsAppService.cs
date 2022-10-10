using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ANPEL.WebDemo.Settings
{
    /// <summary>
    /// 设置
    /// </summary>
    public interface IWebDemoSettingsAppService : IApplicationService
    {
        Task<WebDemoSettingsDto> GetAsync();

        Task UpdateAsync(WebDemoSettingsUpdateDto input);
    }
}
