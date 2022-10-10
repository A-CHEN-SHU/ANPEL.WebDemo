using System;
using System.Collections.Generic;

namespace ANPEL.WebDemo.Pays
{
    public class WxPayOptions
    {
        public string nativeUrl { set; get; }// 支付接口
        public  string mchid { set; get; }// 商户Id
        public  string certpath { set; get; } // 商户证书路径
        public  string certSerialNo { set; get; }// 证书序列号

        public WxPayOptions()
        {
            nativeUrl = "";
            mchid = "";
            certpath = "";
            certSerialNo = "";
        }
    }
}
