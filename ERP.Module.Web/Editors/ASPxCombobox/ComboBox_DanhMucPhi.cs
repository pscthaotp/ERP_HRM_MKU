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
    public class ComboBox_DanhMucPhi : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_DanhMucPhi(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

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

            _aspxComboBox.TextField = "TenLoaiPhi";
            _aspxComboBox.ValueField = "MaLoaiPhi";
            _aspxComboBox.DataSource = UpdateDanhMucPhi();
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
                    if (View.Id.Equals("HoSoXetTuyen_DangKyDichVu_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            HoSoXetTuyen_DangKyDichVu obj = View.CurrentObject as HoSoXetTuyen_DangKyDichVu;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.ID_LoaiPhi = Convert.ToInt16((sender as ASPxComboBox).Value);
                                //lấy giá trị của combobox
                                obj.TenLoaiPhi = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = null;
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

        private DataTable UpdateDanhMucPhi()
        {
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon) || _connect.Contains(Config.KeyServerMamNonAzure))
                query = "SELECT FeeDetailTypeID as MaLoaiPhi,FeeDetailTypeName as TenLoaiPhi FROM " + Config.KeyLinkServer + "AccountsFee.dbo.tblFeeDetailTypes where FeeDetailTypeId != 2 and FeeDetailTypeId != 18";
            else
                query = "SELECT FeeDetailTypeID as MaLoaiPhi,FeeDetailTypeName as TenLoaiPhi FROM AccountsFee.dbo.tblFeeDetailTypes where FeeDetailTypeId != 2 and FeeDetailTypeId != 18";
            //
            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                return dt;
            }
        }
    }
}