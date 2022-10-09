using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ANPEL.WebDemo.Order
{
    public class OrderPagedInputDto : PagedResultRequestDto
    {
        /// <summary>
        /// 筛选条件
        /// </summary>
        public decimal ItemPrice { get; set; }
    }
}
