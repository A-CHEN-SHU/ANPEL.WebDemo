using ANPEL.WebDemo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ANPEL.WebDemo.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class WebDemoController : AbpController
    {
        protected WebDemoController()
        {
            LocalizationResource = typeof(WebDemoResource);
        }
    }
}