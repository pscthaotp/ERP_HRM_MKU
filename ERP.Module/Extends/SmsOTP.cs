using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using ERP.Module.HeThong;
using ERP.Module.Enum.Systems;
using System.Security.Cryptography;
using ERP.Module.WebReferences.SmsGateway;
using Newtonsoft.Json;
using ERP.Module.DTO;
//
namespace ERP.Module.Extends
{
    public static class SmsOTP
    {
        public static bool SendSMS(string receiver,string content)
        {
            //
            bool sucess = false;   
            string AccountID = "tester";
            string AppID = "apptest";
            string AccountAuthenCode = "0sc4ag99";
            
            //Gửi
            string confirmCode = MD5Text(AccountAuthenCode + "$" + AccountID + "$%" + AppID + "@&*" + content);
            string jsonResponse = getAppService().sendSMS(AccountID, AppID, receiver, content, confirmCode);

            DTO_SmsGateway res = JsonConvert.DeserializeObject<DTO_SmsGateway>(jsonResponse);
            if (res.responseCode == "OK")
                sucess = true;

            return sucess;
        }
        public static bool RequestOTP(string receiver)
        {
            //
            bool sucess = false;
            string AccountID = "tester";
            string AppID = "apptest";
            string AccountAuthenCode = "0sc4ag99";

            //Gửi
            string confirmCode = MD5Text(AccountAuthenCode + "$" + AccountID + "$%" + AppID + "$&^%" + receiver);
            string jsonResponse = getAppService().requestOTP(AccountID, AppID, receiver, confirmCode);

            DTO_SmsGateway res = JsonConvert.DeserializeObject<DTO_SmsGateway>(jsonResponse);
            if (res.responseCode == "OK")
                sucess = true;

            return sucess;
        }

        public static string getMd5Hash(string text)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static appservice getAppService()
        {
            string AppServiceURL = ERP.Module.Properties.Resources.url_smsgateway_appservice;
            appservice service = new appservice();
            service.Credentials = System.Net.CredentialCache.DefaultCredentials;
            service.Url = AppServiceURL;
            return service;
        }
        public static string MD5Text(string text)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "MD5");
        }

    }
}
