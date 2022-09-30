using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ANPEL.WebDemo.Product
{
    public class ProductPagedInputDto : PagedResultRequestDto
    {
        /// <summary>
        /// 筛选条件
        /// </summary>
        public decimal ProductPrice { get; set; }
    }
}
