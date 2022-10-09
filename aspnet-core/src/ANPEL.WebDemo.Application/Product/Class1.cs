using Microsoft.Extensions.DependencyInjection;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ANPEL.WebDemo.Product
{
    [Dependency(ServiceLifetime.Singleton)]
    public class Class1
    {
        /// <summary>
        /// 获取雪花ID
        /// </summary>
        /// <returns></returns>
        public long GetIdWorker()
        {
            IdWorker IdWorker = new IdWorker(1,1);
            return IdWorker.NextId();
        }
    }
}
