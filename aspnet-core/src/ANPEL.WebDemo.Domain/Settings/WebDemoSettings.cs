namespace ANPEL.WebDemo.Settings
{
    public static class WebDemoSettings
    {
        private const string Prefix = "WebDemo";

        //Add your own setting names here. Example:
        //public const string MySetting1 = Prefix + ".MySetting1";

        /// <summary>
        /// 微信支付设置
        /// </summary>
        public static class WxPay
        {
            public const string Default = Prefix + ".WxPay";
            public const string NativeUrl = Default + ".nativeUrl";
            public const string Mchid = Default + ".mchid";
            public const string Certpath = Default + ".certpath";
            public const string CertSerialNo = Default + ".certSerialNo";
        }
        //public static class ZfbPay
        //{
        //    public const string Default = Prefix + ".ZfbPay";
        //    public const string NativeUrl = Default + ".nativeUrl";
        //    public const string Mchid = Default + ".mchid";
        //    public const string Certpath = Default + ".certpath";
        //    public const string CertSerialNo = Default + ".certSerialNo";
        //}
    }
}