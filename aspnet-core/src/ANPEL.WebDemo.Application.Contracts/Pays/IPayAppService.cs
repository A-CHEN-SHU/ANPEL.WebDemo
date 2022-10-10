using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ANPEL.WebDemo.Pays
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public interface IPayAppService : IApplicationService
    {
        /// <summary>
        /// 支付创建
        /// </summary>
        /// <param name="OrderSn"></param>
        /// <param name="OrderPrice"></param>
        /// <returns></returns>
        public string CreatePay(string productName, string orderSn, string totalPrice);
    }
}
