using System;
using System.Collections.Generic;
using System.Text;
using ANPEL.WebDemo.Localization;
using Volo.Abp.Application.Services;

namespace ANPEL.WebDemo
{
    /* Inherit your application services from this class.
     */
    public abstract class WebDemoAppService : ApplicationService
    {
        protected WebDemoAppService()
        {
            LocalizationResource = typeof(WebDemoResource);
        }
    }
}
