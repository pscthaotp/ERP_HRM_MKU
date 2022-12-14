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
    public class ComboBox_HeThiLai : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_HeThiLai(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

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
            
            _aspxComboBox.TextField = "TENHE";
            _aspxComboBox.ValueField = "ID";
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
                    if (View.Id.Equals("ToChucThi_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            ToChucThi obj = View.CurrentObject as ToChucThi;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.ID_HE_ThiLai = int.Parse((sender as ASPxComboBox).Value.ToString());
                                //lấy giá trị của combobox
                                obj.HeDaoTaoSIS_ThiLai = (sender as ASPxComboBox).Text.ToString();
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

        private DataTable UpdateDanhMucHe()
        {
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon) || _connect.Contains(Config.KeyServerMamNonAzure))
                query = "select ID, TENHE, ID_TRUONG from " + Config.KeyLinkServer + "SIS.dbo.HEDAOTAO";
            else
            {
                if (user.CongTy.Oid.Equals(Config.KeyTanPhu))
                {
                    query = "select ID, TENHE, ID_TRUONG from SIS.dbo.HEDAOTAO WHERE ID_TRUONG = 1";
                }
                else if (user.CongTy.Oid.Equals(Config.KeyThaiBinhDuong))
                {
                    query = "select ID, TENHE, ID_TRUONG from SIS.dbo.HEDAOTAO WHERE ID_TRUONG = 19";
                }
                else
                    query = "select ID, TENHE, ID_TRUONG from SIS.dbo.HEDAOTAO";
            }
            //
            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if (View.Id.Equals("ToChucThi_DetailView"))
                        {
                            ToChucThi current = View.CurrentObject as ToChucThi;
                            if (current != null)
                            {
                                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                                DanhMucHe obj = new DanhMucHe(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenHe = item["TENHE"].ToString();
                                    obj.ID_Truong = int.Parse(item["ID_TRUONG"].ToString());
                                }
                                current.ListDanhMucHe_ThiLai = new XPCollection<DanhMucHe>(session, false);
                                current.ListDanhMucHe_ThiLai.Add(obj);
                            }
                        }
                    }
                    return dt;
                }
                else
                    return dt;
            }
        }
    }
}