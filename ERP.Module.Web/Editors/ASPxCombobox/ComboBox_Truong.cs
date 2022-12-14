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
    public class ComboBox_Truong : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_Truong(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

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

            _aspxComboBox.TextField = "TENTRUONG";
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
                    if (View.Id.Equals("HoSoXetTuyen_DetailView") || View.Id.Equals("DanhSachTre_DetailView"))
                    {
                        if (View.Id.Equals("HoSoXetTuyen_DetailView"))
                        {
                            HoSoXetTuyen obj = View.CurrentObject as HoSoXetTuyen;
                            if ((sender as ASPxComboBox).Value != null
                               && (sender as ASPxComboBox).Text != null)
                            {
                               
                                if (obj != null)
                                {
                                    //Lấy id
                                    obj.ID_TruongSIS = int.Parse((sender as ASPxComboBox).Value.ToString());
                                    //lấy giá trị của combobox
                                    obj.TruongSIS = (sender as ASPxComboBox).Text.ToString();
                                }
                            }
                            else
                            {
                                PropertyValue = obj.TruongSIS;
                            }
                        }
                        if (View.Id.Equals("DanhSachTre_DetailView"))
                        {
                            DanhSachTre obj = View.CurrentObject as DanhSachTre;
                            if ((sender as ASPxComboBox).Value != null
                        && (sender as ASPxComboBox).Text != null)
                            {
                              
                                if (obj != null)
                                {
                                    //Lấy id
                                    obj.ID_TruongSIS = int.Parse((sender as ASPxComboBox).Value.ToString());
                                    //lấy giá trị của combobox
                                    obj.TruongSIS = (sender as ASPxComboBox).Text.ToString();
                                }
                            }
                            else
                            {
                                PropertyValue = obj.TruongSIS;
                            }
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
            var query = "select tr.id, TENTRUONG + isnull(' ('+ q.TENQUAN +')','') as TENTRUONG from SIS.dbo.TRUONG tr left join SIS.dbo.QUAN q on q.ID = tr.ID_QUAN";
            if (_connect.Contains(Config.KeyServerMamNon))
            {
                SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("SIS_TTC_ID.bin"));
                cnn.Open();
                //query = "select tr.id, TENTRUONG + isnull(' ('+ q.TENQUAN +')','') as TENTRUONG from SIS.dbo.TRUONG tr left join SIS.dbo.QUAN q on q.ID = tr.ID_QUAN";
                using (DataTable dt = DataProvider.GetDataTableFromSQL(cnn, query, CommandType.Text))
                {
                    cnn.Close();
                    return dt;
                }
            }
            else if(_connect.Contains(Config.KeyServerMamNonAzure))
            {
                SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("SIS_TTC_ID_Azure.bin"));
                cnn.Open();
                //query = "select tr.id, TENTRUONG + isnull(' ('+ q.TENQUAN +')','') as TENTRUONG from SIS.dbo.TRUONG tr left join SIS.dbo.QUAN q on q.ID = tr.ID_QUAN";
                using (DataTable dt = DataProvider.GetDataTableFromSQL(cnn, query, CommandType.Text))
                {
                    cnn.Close();
                    return dt;
                }
            }
            else
            {
                //query = "select tr.id, TENTRUONG +' ('+q.TENQUAN+')' as TENTRUONG from SIS.dbo.TRUONG tr left join SIS.dbo.QUAN q on q.ID = tr.ID_QUAN";
                using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
                {
                    return dt;
                }
            }
        }
    }
}