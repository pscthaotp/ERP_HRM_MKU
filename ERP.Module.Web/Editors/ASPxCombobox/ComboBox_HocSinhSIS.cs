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
//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ComboBox_HocSinhSIS : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        ASPxComboBox _aspxComboBox2 = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_HocSinhSIS(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

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

            _aspxComboBox.TextField = "HOTEN";
            _aspxComboBox.ValueField = "MAHOCSINH";
            _aspxComboBox.DataSource = UpdateDanhMucHe();
            _aspxComboBox.DataBind();
            //hiện nút clear
            _aspxComboBox.ClearButton.DisplayMode = ClearButtonDisplayMode.Always;
            //
            _aspxComboBox.DropDownButton.Visible = this.AllowEdit;
            _aspxComboBox.SelectedIndexChanged += new EventHandler(_aspxComboBox_SelectedIndexChanged);
            //
            return _aspxComboBox;
        }

        void _aspxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AllowEdit)
            {
                try
                {
                    if (View.Id.Equals("ChiTietToChucSuKien_HocSinh_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            if (View.Id.Equals("ChiTietToChucSuKien_HocSinh_DetailView"))
                            {
                                ChiTietToChucSuKien_HocSinh obj = View.CurrentObject as ChiTietToChucSuKien_HocSinh;
                                if (obj != null)
                                {
                                    //Lấy id
                                    obj.MAHOCSINH_SIS = (sender as ASPxComboBox).Value.ToString();
                                    //lấy giá trị của combobox
                                    obj.HOTEN_SIS = (sender as ASPxComboBox).Text.ToString();
                                    //
                                }
                            }
                        }
                        else
                        {
                            PropertyValue = null;
                        }
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

        private DataTable UpdateDanhMucHe()
        {
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon) || _connect.Contains(Config.KeyServerMamNonAzure))
                query = "select ID, HOVALOT + TEN as HOTEN , MAHOCSINH, LOPDANGHOC from " + Config.KeyLinkServer + "SIS.dbo.HocSinh";
            else
                query = "select ID, HOVALOT + TEN as HOTEN, MAHOCSINH, LOPDANGHOC from SIS.dbo.HocSinh";
            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                return dt;
            }
        }
    }
}