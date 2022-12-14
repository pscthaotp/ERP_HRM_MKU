using System;
using System.Globalization;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Model;
using DevExpress.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Web.ASPxHtmlEditor;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.TuyenSinh;
//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class Html_NoiDungGuiMail : ASPxPropertyEditor
    {
        ASPxHtmlEditor _aspxHtmlControll = new ASPxHtmlEditor();
        //
        public Html_NoiDungGuiMail(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

        protected override void SetupControl(WebControl control)
        {
            if (ViewEditMode == ViewEditMode.Edit)
            {

            }
        }
        protected override WebControl CreateEditModeControlCore()
        {
            _aspxHtmlControll = new ASPxHtmlEditor();
            _aspxHtmlControll.SettingsText.DesignViewTab = "Soạn mẫu";
            _aspxHtmlControll.SettingsText.PreviewTab = "Kết quả";
            //Set chiều cao
            SetHeight();
            //Set dữ liệu
            SetHtml();
            //
            _aspxHtmlControll.HtmlChanged += HtmlChanged;
            //
            return _aspxHtmlControll;
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            //
            base.BreakLinksToControl(unwireEventsOnly);
        }
        void HtmlChanged(object sender, EventArgs e)
        {
            if (View.Id.Equals("ToChucSuKien_ThucHien_DetailView"))
            {
                ToChucSuKien_ThucHien obj = View.CurrentObject as ToChucSuKien_ThucHien;
                if (obj != null)
                    obj.NoiDung = _aspxHtmlControll.Html.ToString();
            }
            if (View.Id.Equals("ThongBaoNhapHoc_TongHop_DetailView"))
            {
                ThongBaoNhapHoc_TongHop obj = View.CurrentObject as ThongBaoNhapHoc_TongHop;
                if (obj != null)
                    obj.NoiDung = _aspxHtmlControll.Html.ToString();
            }
            if (View.Id.Equals("TuVanTuyenSinh_TongHop_DetailView"))
            {
                TuVanTuyenSinh_TongHop obj = View.CurrentObject as TuVanTuyenSinh_TongHop;
                if (obj != null)
                    obj.NoiDung = _aspxHtmlControll.Html.ToString();
            }
            if (View.Id.Equals("TuVanTuyenSinh_ThucHien_DetailView"))
            {
                TuVanTuyenSinh_ThucHien obj = View.CurrentObject as TuVanTuyenSinh_ThucHien;
                if (obj != null)
                    obj.NoiDung = _aspxHtmlControll.Html.ToString();
            }
            if (View.Id.Equals("ThongBaoNhapHoc_ThucHien_DetailView"))
            {
                ThongBaoNhapHoc_ThucHien obj = View.CurrentObject as ThongBaoNhapHoc_ThucHien;
                if (obj != null)
                    obj.NoiDung = _aspxHtmlControll.Html.ToString();
            }
            if (View.Id.Equals("ThongBaoNhapHoc_Mau_DetailView"))
            {
                ThongBaoNhapHoc_Mau obj = View.CurrentObject as ThongBaoNhapHoc_Mau;
                if (obj != null)
                {
                    //
                    obj.NoiDungGuiMail = _aspxHtmlControll.Html.ToString();
                    _aspxHtmlControll.Html = obj.NoiDung;
                }
            }
            if (View.Id.Equals("CauHinhChung_DetailView"))
            {
                CauHinhChung obj = View.CurrentObject as CauHinhChung;
                if (obj != null)
                    obj.CauHinhMail.Template = _aspxHtmlControll.Html.ToString();
            }
            if (View.Id.Equals("CauHinhChung_DetailView"))
            {
                CauHinhChung obj = View.CurrentObject as CauHinhChung;
                if (obj != null)
                    obj.CauHinhMail.Template = _aspxHtmlControll.Html.ToString();
            }
        }

        void SetHtml()
        {
            if (View.Id.Equals("ThongBaoNhapHoc_Mau_DetailView"))
            {
                var user = Common.SecuritySystemUser_GetCurrentUser();
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                CauHinhMail template = session.FindObject<CauHinhMail>(CriteriaOperator.Parse("Oid = ?", user.CongTy.CauHinhChung.CauHinhMail.Oid));
                if (template != null)
                {
                    ThongBaoNhapHoc_Mau obj = View.CurrentObject as ThongBaoNhapHoc_Mau;
                    if (obj != null)
                    {
                        if (obj.HocSinh == null)
                        {
                            _aspxHtmlControll.Html = template.Template;
                            obj.NoiDung = template.Template;
                        }
                    }
                }
            }
            else if (View.Id.Equals("CauHinhChung_DetailView"))
            {

                var user = Common.SecuritySystemUser_GetCurrentUser();
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                CauHinhMail template = session.FindObject<CauHinhMail>(CriteriaOperator.Parse("Oid = ?", user.CongTy.CauHinhChung.CauHinhMail.Oid));
                if (template != null)
                {
                    _aspxHtmlControll.Html = template.Template;
                }
            }
            else if (View.Id.Equals("MailManager_DetailView"))
            {
                //
                MailManager obj = View.CurrentObject as MailManager;
                if (obj != null)
                    _aspxHtmlControll.Html = obj.Contents;
            }
            else if (View.Id.Equals("ChiTietThongBaoNhapHoc_DetailView"))
            {
                //
                ChiTietThongBaoNhapHoc obj = View.CurrentObject as ChiTietThongBaoNhapHoc;
                if (obj != null)
                    _aspxHtmlControll.Html = obj.NoiDung;
            }
        }
        void SetHeight()
        {
            if (View.Id.Equals("ThongBaoNhapHoc_Mau_DetailView"))
            {
                _aspxHtmlControll.Height = 500;
            }
            else
            {
                _aspxHtmlControll.Height = 250;
            }
        }
    }
}