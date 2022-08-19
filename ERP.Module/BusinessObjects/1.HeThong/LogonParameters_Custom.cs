using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Web;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.HeThong
{
    [ModelDefault("Caption", "Đăng nhập")]
    [Appearance("Hide", TargetItems = "LoaiXacThuc;MaXacThuc;LayXacThuc", Visibility = ViewItemVisibility.Hide, Criteria = "!XacThuc")]
    public class LogonParameters_Custom :  INotifyPropertyChanged, ISerializable
    {
        private string _UserName;
        private string _PassWord;
        public static ASPxCaptcha _Captcha = null;
        public static LoaiXacThucEnum _LoaiXacThuc = LoaiXacThucEnum.SMS;
        private string _MaXacThuc;
        private string _LayXacThuc = string.Empty;

       [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string UserName     
       {
            get 
            {
                return _UserName; 
            }
            set
            {
                _UserName = value;
            }
        }

        [ModelDefault("Caption", "Mật khẩu")]
        [PasswordPropertyText(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string PassWord 
        {
            get 
            { 
                return _PassWord; 
            }
            set
            {
                _PassWord = value;
            }
        }

        [ModelDefault("Caption", "Mã xác thực")]
        [RuleRequiredField(DefaultContexts.Save,TargetCriteria ="XacThuc")]
        public string MaXacThuc
        {
            get
            {
                return _MaXacThuc;
            }
            set
            {
                _MaXacThuc = value;
            }
        }

        [ModelDefault("Caption", "Loại xác thực")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "XacThuc")]
        public LoaiXacThucEnum LoaiXacThuc
        {
            get
            {
                return _LoaiXacThuc;
            }
            set
            {
                _LoaiXacThuc = value;
            }
        }

        [ModelDefault("Caption", "Lấy mã")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Web.Editors.Button_LayXacThuc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "XacThuc")]
        public string LayXacThuc
        {
            get
            {
                return _LayXacThuc;
            }
            set
            {
                _LayXacThuc = value;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Xác thực")]
        public bool XacThuc
        {
            get
            {
                //
                return Convert.ToBoolean(Config.Accuracy);
            }
        }

        public LogonParameters_Custom() { }

        public LogonParameters_Custom(SerializationInfo info, StreamingContext context)
        {
            if (info.MemberCount > 0)
            {
                UserName = info.GetString("UserName");
                PassWord = info.GetString("PassWord");
                MaXacThuc = info.GetString("MaXacThuc ");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [System.Security.SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserName", UserName);
            info.AddValue("PassWord", PassWord);
            info.AddValue("MaXacThuc", PassWord);
        }

        public bool KiemTraCaptcha()
        {
            if (_Captcha == null || _Captcha.IsValid)
                return true;
            else
                return false;
        }
    }  
}
