using ANPEL.WebDemo.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace ANPEL.WebDemo.Pays
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WxPayAppService : WebDemoAppService, IPayAppService
    {
        public WxPayHttpClient _wxPayHttpClient { set; get; }

        protected WxPayOptions _wxPayOptions { get; }
        public WxPayAppService(IOptions<WxPayOptions> wxPayOptions)
        {
            _wxPayOptions = wxPayOptions.Value;
        }

        public string CreatePay(string productName, string orderSn, string totalPrice)
        {
            #region 
            {

                // 1、创建支付对象
                NativePay nativePay = new NativePay();
                nativePay.description = productName;
                nativePay.out_trade_no = orderSn;
                nativePay.amount.total = int.Parse(float.Parse(totalPrice) * 100 + "");

                // 2、支付对象转换成json
                string nativePayJson = JsonConvert.SerializeObject(nativePay);

                // 3、创建支付
                string result = _wxPayHttpClient.
                    WeChatPostAsync(SettingProvider.GetOrNullAsync(WebDemoSettings.WxPay.NativeUrl).Result,
                    nativePayJson,
                    SettingProvider.GetOrNullAsync(WebDemoSettings.WxPay.Certpath).Result,
                    SettingProvider.GetOrNullAsync(WebDemoSettings.WxPay.Mchid).Result,
                   SettingProvider.GetOrNullAsync(WebDemoSettings.WxPay.CertSerialNo).Result).Result;

                return result;
            }
            #endregion
        }
    }
}
