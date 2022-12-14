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
using ERP.Module.DanhMuc.NhanSu;
//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ComboBox_ChinhSachMienGiam : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_ChinhSachMienGiam(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

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

            _aspxComboBox.TextField = "TENCHINHSACH";
            _aspxComboBox.ValueField = "MACHINHSACH";
            _aspxComboBox.DataSource = UpdateChinhSachMienGiam();
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
                    if (View.Id.Equals("HoSoXetTuyen_DetailView"))
                    {

                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            HoSoXetTuyen obj = View.CurrentObject as HoSoXetTuyen;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.MACHINHSACH = (sender as ASPxComboBox).Value.ToString();
                                //lấy giá trị của combobox
                                obj.TENCHINHSACH = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = null;
                    }
                    if (View.Id.Equals("ThongBaoTuyenSinh_UuDai_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            ThongBaoTuyenSinh_UuDai obj = View.CurrentObject as ThongBaoTuyenSinh_UuDai;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.MACHINHSACH = (sender as ASPxComboBox).Value.ToString();
                                //lấy giá trị của combobox
                                obj.TENCHINHSACH = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = null;
                    }
                    if (View.Id.Equals("HoSoXetTuyen_ListChinhSachMienGiam_DetailView"))
                    {
                        HoSoXetTuyen_ChinhSachMienGiam obj = View.CurrentObject as HoSoXetTuyen_ChinhSachMienGiam;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {

                            if (obj != null)
                            {
                                //Lấy id
                                obj.MaChinhSach = (sender as ASPxComboBox).Value.ToString();
                                //lấy giá trị của combobox
                                obj.TenChinhSach = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = obj.MaChinhSach;
                    }
                    if (View.Id.Equals("XetMienGiam_DetailView"))
                    {
                        XetMienGiam obj = View.CurrentObject as XetMienGiam;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {

                            if (obj != null)
                            {
                                //Lấy id
                                obj.MaChinhSach = (sender as ASPxComboBox).Value.ToString();
                                //lấy giá trị của combobox
                                obj.TenChinhSach = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = obj.MaChinhSach;
                    }

                    if (View.Id.Equals("DanhMucXetMienGiam_Khoi_DetailView"))
                    {
                        DanhMucXetMienGiam_Khoi obj = View.CurrentObject as DanhMucXetMienGiam_Khoi;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {

                            if (obj != null)
                            {
                                //Lấy id
                                obj.MaChinhSach = (sender as ASPxComboBox).Value.ToString();
                                //lấy giá trị của combobox
                                obj.TenChinhSach = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = obj.MaChinhSach;
                    }

                    if (View.Id.Equals("HoSoXetTuyen_ChinhSachMienGiam_DetailView"))
                    {
                        HoSoXetTuyen_ChinhSachMienGiam obj = View.CurrentObject as HoSoXetTuyen_ChinhSachMienGiam;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {

                            if (obj != null)
                            {
                                //Lấy id
                                obj.MaChinhSach = (sender as ASPxComboBox).Value.ToString();
                                //lấy giá trị của combobox
                                obj.TenChinhSach = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = obj.MaChinhSach;
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

        private DataTable UpdateChinhSachMienGiam()
        {
            var query = "";
            SqlConnection cnn = new SqlConnection();
            if (_connect.Contains(Config.KeyServerMamNon) || _connect.Contains(Config.KeyServerMamNonAzure))
            {
                if (_connect.Contains(Config.KeyServerMamNon))
                {
                    query = "SELECT MaChinhSach, TenChinhSach from " + Config.KeyLinkServer + "AccountsFee.dbo.tblChinhSach where TenChinhSach != ''";
                    cnn = new SqlConnection(DataProvider.GetConnectionString("AccountFee_TTC_ID.bin"));
                }
                if (_connect.Contains(Config.KeyServerMamNonAzure))
                {
                    query = "SELECT MaChinhSach, TenChinhSach from " + Config.KeyLinkServer + ".AccountsFee.dbo.tblChinhSach where TenChinhSach != ''";
                    //int bien = 0;
                    cnn = new SqlConnection(DataProvider.GetConnectionString("AccountFee_TTC_ID_Azure.bin"));
                }
                cnn.Open();
                HoSoXetTuyen_ChinhSachMienGiam hs = View.CurrentObject as HoSoXetTuyen_ChinhSachMienGiam;
                SqlParameter[] param = new SqlParameter[1];

                //Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                string namhoc;

                if (hs != null && hs.HoSoXetTuyen != null)
                    namhoc = hs.HoSoXetTuyen.NamHoc != null ? hs.HoSoXetTuyen.NamHoc.TenNamHoc : "";
                else
                    namhoc = "";
                param[0] = new SqlParameter("@NamHoc", namhoc);
                SqlCommand cmd = DataProvider.GetCommand("sp_psc_tblChinhSach_GetKhoiDaoTaoByIdHe", CommandType.StoredProcedure, param);
                using (DataTable dt = DataProvider.ExecuteNonQueryReturnTable(cnn, cmd))
                    return dt;
                cnn.Close();
            }
            else
            {
                SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
                HoSoXetTuyen_ChinhSachMienGiam hs = View.CurrentObject as HoSoXetTuyen_ChinhSachMienGiam;
                string namhoc;
                if (hs != null && hs.HoSoXetTuyen != null)
                    namhoc = hs.HoSoXetTuyen.NamHoc != null ? hs.HoSoXetTuyen.NamHoc.TenNamHoc : "";
                else
                    namhoc = "";
                if (user.CongTy.Oid.Equals(Config.KeyTanPhu))
                {
                    query = string.Format("SELECT MaChinhSach, TenChinhSach, TuNgay, DenNgay from AccountsFee.dbo.tblChinhSach where NamHoc='{0}' AND MaTruong ='{1}'", namhoc, 1);
                }
                else if (user.CongTy.Oid.Equals(Config.KeyThaiBinhDuong))
                {
                    query = string.Format("SELECT MaChinhSach, TenChinhSach, TuNgay, DenNgay from AccountsFee.dbo.tblChinhSach where NamHoc='{0}' AND MaTruong ='{1}'", namhoc, 19);
                }
                else
                    query = string.Format("SELECT MaChinhSach, TenChinhSach, TuNgay, DenNgay from AccountsFee.dbo.tblChinhSach where NamHoc='{0}'", namhoc);
                using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
                {
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow item in dt.Rows)
                    //    {
                    //        if (View.Id.Equals("HoSoXetTuyen_DetailView"))
                    //        {
                    //            if (hs != null)
                    //            {
                    //                if (!item.IsNull("ID"))
                    //                {
                    //                    hs.MaChinhSach = item["MaChinhSach"].ToString();
                    //                    hs.TenChinhSach = item["TenChinhSach"].ToString();
                    //                    hs.TuNgay = DateTime.Parse(item["TuNgay"].ToString());
                    //                    hs.DenNgay = DateTime.Parse(item["DenNgay"].ToString());
                    //                }
                    //            }
                    //        }
                    //    }
                    //    return dt;
                    //}
                    //else
                        return dt;
                }
            }
        }
    }
}