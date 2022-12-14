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
using System.Drawing;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using ERP.Module.DanhMuc.TuyenSinh_TP;
using ERP.Module.DanhMuc.TuyenSinh;
//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ComboBox_KhoiKey : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_KhoiKey(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

        protected override void SetupControl(WebControl control)
        {
            if (ViewEditMode == ViewEditMode.Edit)
            {

            }
        }
        protected override WebControl CreateEditModeControlCore()
        {
            _aspxComboBox = new ASPxComboBox();
            //Set null value
            _aspxComboBox.NullText = "Chưa chọn";
            //đổ dữ liệu vào combobox 
            DataTable khoiList = KhoiList();
            //
            _aspxComboBox.DataSource = khoiList;
            _aspxComboBox.DataBind();
            _aspxComboBox.TextField = "TENKHOI";
            _aspxComboBox.ValueField = "TENKHOI";

            //hiện nút clear
            _aspxComboBox.ClearButton.DisplayMode = ClearButtonDisplayMode.Always;
            //
            _aspxComboBox.DropDownButton.Visible = this.AllowEdit;
            _aspxComboBox.SelectedIndexChanged += new EventHandler(_aspxComboBox_SelectedIndexChanged);
            return _aspxComboBox;
        }

        void _aspxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AllowEdit)
            {
                try
                {
                    if (View.Id.Equals("KetQuaCuoiNam_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            KetQuaCuoiNam obj = View.CurrentObject as KetQuaCuoiNam;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KHOI_SIS_KEY = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }
                }
                catch (Exception ex)
                {
                }
                OnControlValueChanged();
            }
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            //
            base.BreakLinksToControl(unwireEventsOnly);
        }

        private DataTable KhoiList()
        {
            //
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon) || _connect.Contains(Config.KeyServerMamNonAzure))
                query = "select ID , TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN ,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from " + Config.KeyLinkServer + "SIS.dbo.KHOI";
            else
                query = "select ID , TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN ,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from SIS.dbo.KHOI";

            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    if (View.Id.Equals("KetQuaCuoiNam_DetailView"))
                    {
                        //
                        KetQuaCuoiNam current = View.CurrentObject as KetQuaCuoiNam;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoiKey = null;
                        current.ListDanhMucKhoiKey = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoiKey.Add(obj);
                            }
                        }
                    }
                }
                return dt;
            }
        }
    }
}