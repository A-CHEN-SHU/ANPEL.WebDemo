using ANPEL.WebDemo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ANPEL.WebDemo.Permissions
{
    public class WebDemoPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(WebDemoPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(WebDemoPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WebDemoResource>(name);
        }
    }
}
